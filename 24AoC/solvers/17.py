from functools import partial
r,ins = open('in/17.txt').read().split('\n\n')
r = [int(x.split(': ')[1]) for x in r.split('\n')]
i = [int(a) for a in ins.split(': ')[1].split(',')]
p = [-2]
res = ""
comb = lambda x: x if x <= 3 else r[x-4]
dv = lambda ri,o: r.__setitem__(ri,r[0]//(2**comb(o)))
bxl = lambda o: r.__setitem__(1,r[1] ^ o)
bst = lambda o: r.__setitem__(1,comb(o)%8)
jnz = lambda p,o: p.__setitem__(0,o-2) if r[0] != 0 else None
out = lambda o:  str(comb(o)%8) + ','
bxc = lambda o: r.__setitem__(1,r[1] ^ r[2])
os = [partial(dv,0),bxl,bst,partial(jnz,p),bxc,out,partial(dv,1),partial(dv,2)]
while 0 <= (p.__setitem__(0,p[0]+2) or p[0]) < len(i): res += '' if (x := os[i[p[0]]](i[p[0]+1])) == None else x
print(res[:-1])
ass=[0]
test = lambda f,ass: [[x for x in range(8) if f == ((x^6)^((a*8+x) // (2**(x^3))))%8] for a in ass]
for ins in i[::-1]: ass = sum([[a*8 + b for b in AX] for a,AX in zip(ass,test(ins,ass))],[])
print(min(ass))