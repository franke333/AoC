e = enumerate
inp = [[int(x) for x in l] for l in open('in/10.txt').read().split('\n')]
ok = lambda x: all(0 <= y < len(inp) for y in x)
o = [(x,y) for y,l in e(inp) for x,g in e(l) if int(g)==0 ]
oo = lambda o: [[int(o[0] == x and o[1] == y) for x in range(len(inp))] for y in range(len(inp))]
step = lambda x,m: sum(m[a][b] for dx in [(1,0),(-1,0),(0,1),(0,-1)] if ok((a := x[0]+dx[0], b := x[1]+dx[1])))
trails = lambda m: sum([m := [[(step((y,x),m) if g==h else 0) for x,g in e(l)] for y,l in e(inp)] for h in range(1,10)][-1],[])
print(sum(sum(map(lambda x: int(x>0),trails(m))) for m in map(oo,o)))
print(sum(sum(trails(m)) for m in map(oo,o)))