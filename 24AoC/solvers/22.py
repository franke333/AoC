from itertools import product
inp =[int(n) for n in open('in/22.txt').read().split('\n')]
a = lambda x: ((64*x) ^ x)%16777216
b = lambda x: ((x//32)^ x)%16777216
c = lambda x: ((2048*x) ^ x)%16777216
op = lambda x: c(b(a(x)))
prices = [[x%10] for x in inp]
for i in range(2000):
    inp = [op(x) for x in inp]
    [prices[j].append(inp[j]%10) for j in range(len(inp))]
print(sum(inp))
seq = [[y-x for x,y in zip(s,s[1:])] for s in prices]
fo = [dict() for _ in seq]
for n,s in enumerate(seq):
    for b,a in enumerate(zip(s,s[1:],s[2:],s[3:])):
        if a not in fo[n].keys():
            fo[n][a] = prices[n][b+4]
ex = lambda d,seq: d[seq] if seq in d.keys() else 0
m = 0
for s in product(range(-9,10),repeat=4):
    m = max(m,sum([ex(d,tuple(s)) for d in fo]))
print(m)