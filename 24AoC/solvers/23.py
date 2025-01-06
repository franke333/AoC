inp = open('in/23.txt').read().split('\n')
m = {}
add = lambda x,y: m[x].add(y) if x in m.keys() else m.__setitem__(x,set([y]))
[add(x,y) or add(y,x) for x,y in [x.split('-') for x in inp]]
s = set()
for k in m.keys():
    if k[0] == 't':
        for l in m[k]:
            for z in m[k].intersection(m[l]):
                s.add(tuple(sorted([k,l,z])))
print(len(s))

def BronKerbosch(R,P,X):
    if len(P) == 0 and len(X) == 0: return R
    ret = []
    for v in P.copy():
        ret = r if len(r:= BronKerbosch(R.union(set([v])),P.intersection(m[v]),X.intersection(m[v]))) > len(ret) else ret
        P.remove(v)
        X.add(v)
    return ret
bk = BronKerbosch(set(),set(m.keys()),set())
print(','.join(sorted(bk)))