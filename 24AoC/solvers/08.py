inp = open("in/08.txt").readlines()
m = dict([(chr(i),[]) for i in range(48,123)])
[m[c].append((x,y)) for y,l in enumerate(inp) for x,c in enumerate(l) if c not in ['\n','.']]
ok = lambda x : all(0 <= y < len(inp) for y in x)
print(len(set([n for l in m.values() for a,b in l for c,d in l if ok(n := (2*a-c,2*b-d)) and ((a,b) != (c,d))])))
line = lambda a,b,c,d : [(a,b) for _ in range(50) if ok((a := a + c,b := b + d))]
print(len(set(sum([line(c,d,*n) for l in m.values() for a,b in l for c,d in l if (n := (a-c,b-d)) != (0,0)],[]))))