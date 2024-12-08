inp = open("in/06.txt").readlines(); S = len(inp)
o = [(y,x) for x in range(S) for y in range(S) if inp[y][x] != "."]
g = ([(y,x) for y,x in o if inp[y][x] == "^"][0],(-1,0))
o.remove(g[0])
out = lambda g : all(a >= 0 and a < S for a in g)
step = lambda g,o : (ng,g[1]) if (ng := tuple(sum(x) for x in zip(g[0],g[1]))) not in o else (g[0],(g[1][1],-g[1][0]))
run = lambda g,o,s=set(): ([s.add(g := step(g,o)) for _ in iter(lambda: out(step(g,o)[0]) and step(g,o) not in s, False)],step(g,o),s)
print(len((p1 := set([s[0] for s in run(g,o)[2]]))))
print(sum([*[out(run(g,o+[(y,x)],set())[1][0]) for y,x in p1 if out((y,x)) and (y,x) != g[0]]]))