inp = open('in/20.txt').read().split('\n')
m = set((x,y)  for y,l in enumerate(open('in/20.txt').read().split('\n')) for x,c in enumerate(l) if c!='#')
S = [(x,y) for x,y in m if inp[y][x]=='S'][0]
E = [(x,y) for x,y in m if inp[y][x]=='E'][0]
d = {}
next = lambda x,y: [a for a in [(x+1,y),(x-1,y),(x,y+1),(x,y-1)] if (a in m and a not in d.keys())][0]
while S!=E: d[S] = len(d); S = next(*S)
d[S] = len(d)
nn = lambda cheat: [(x,y) for x in range(-cheat,cheat+1) for y in range(-cheat,cheat+1) if abs(x)+abs(y)<=cheat]
def solve(cheat):
    shortcuts = [0 for _ in range(len(d))]
    for (x,y),s in d.items():
        for dx,dy in nn(cheat):
            l = abs(dx)+abs(dy)
            if (x+dx,y+dy) in m and s + l < d[(x+dx,y+dy)]:
                shortcuts[d[(x+dx,y+dy)]-s-l] += 1
    return shortcuts
print(sum(solve(2)[100:]))
print(sum(solve(20)[100:]))