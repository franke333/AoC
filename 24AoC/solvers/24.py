start, gates = open('in/24.txt').read().split('\n\n')
locked = dict((w,v=='1') for w,v in [l.split(': ') for l in start.split('\n')])
gates = [(a,b,o,r) for a,o,b,_,r in [l.split(' ') for l in gates.split('\n')]]
gates2 = gates.copy()
ready = lambda a,b: a in locked.keys() and b in locked.keys()
eva = lambda a,b,o: (a and b) if o =='AND' else ((a or b) if o == 'OR' else (a != b))
while len(gates) > 0:
    for a,b,o,r in gates:
        if ready(a,b):
            locked[r] = eva(locked[a],locked[b],o)
            gates.remove((a,b,o,r))
n,s,r = 0,0,[]
while (k := 'z' + str(n).zfill(2)) in locked.keys():
    r.append(locked[k]);  n += 1
for rr in r[::-1]:
    s *= 2; s += rr
print(s)

tstr = lambda c,n: c + str(n).zfill(2)
bits = len(r)-1
add_gates = [('x00','y00','XOR','z00'),('x00','y00','AND','c00')]
for i in range(1,bits):
    add_gates.append((tstr('x',i),tstr('y',i),'XOR',tstr('xor',i)))
    add_gates.append((tstr('x',i),tstr('y',i),'AND',tstr('a',i)))
    add_gates.append((tstr('xor',i),tstr('c',i-1),'XOR',tstr('z',i)))
    add_gates.append((tstr('xor',i),tstr('c',i-1),'AND',tstr('h',i)))
    add_gates.append((tstr('a',i),tstr('h',i),'OR',tstr('c',i)  if i != bits-1 else tstr('z',i+1)))
mapping = dict([(tstr(c,n),[tstr(c,n)]) for n in range(bits) for c in ['x','y','z']] + [(tstr('z',bits),[tstr('z',bits)])])
exert = lambda a: mapping[a] if a in mapping.keys() else []
mapto = lambda a,b,o,r: ([((a,aa),(b,bb),(r,rr)) for aa,bb,oo,rr in add_gates if (aa in exert(a) and bb in exert(b) and oo == o) or \
                            (rr in exert(r) and bb in exert(b) and oo == o) or (aa in exert(a) and rr in exert(r) and oo == o)] + \
                            [((a,aa),(b,bb),(r,rr)) for bb,aa,oo,rr in add_gates if (aa in exert(a) and bb in exert(b) and oo == o) or \
                            (rr in exert(r) and bb in exert(b) and oo == o) or (aa in exert(a) and rr in exert(r) and oo == o)])
while len(gates2) > 0:
    for a,b,o,r in gates2:
        res = mapto(a,b,o,r)
        if len(res) == 0: continue
        change = True
        for f,t in res[0]:
            if f not in mapping.keys(): mapping[f] = [t]
            elif t not in mapping[f]: mapping[f].append(t)
        gates2.remove((a,b,o,r))
problematic = set(sum([[x,y] for x,y in filter(lambda a: len(a) > 1,mapping.values())],[]))
wires = []
for k,r in mapping.items():
    if any([c in problematic for c in r]):
        wires.append(k)
print(",".join(sorted(wires)))