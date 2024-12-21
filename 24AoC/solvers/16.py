inp = open('in/16.txt').read().split('\n')
S = [[y,x] for y,r in enumerate(inp) for x,c in enumerate(r) if c == 'S'][0]
E = [[y,x] for y,r in enumerate(inp) for x,c in enumerate(r) if c == 'E'][0]
h = dict()
rotate = lambda dy,dx,left: [-dx,dy] if left else [dx,-dy]
step = lambda y,x,dy,dx,cost: [] if(inp[y][x] == '#' or h.get((y,x,dy,dx),1e99) <= cost) else \
      [h.__setitem__((y,x,dy,dx),cost),(y+dy,x+dx,dy,dx,cost+1),(y,x,*rotate(dy,dx,1),cost+1000),(y,x,*rotate(dy,dx,0),cost+1000)][1:]
q = [(*S,0,1,0)]
while len(q) > 0: [q.append(s) for s in step(*q.pop(0))]
print(res := min([h.get((*E,dy,dx),1e99) for dx,dy in [[0,1],[1,0],[0,-1],[-1,0]]]))
s = set()
q = [(*E,dy,dx,h[*E,dy,dx]) for dx,dy in [[0,1],[1,0],[0,-1],[-1,0]] if h.get((*E,dy,dx),1e99) == res]
mark = lambda y,x,dy,dx,cost: [] if inp[y][x] == '#' else \
      ([] if h.get((y-dy,x-dx,dy,dx),1e99) != cost-1 else [s.add((y,x)),[(y-dy,x-dx,dy,dx,cost-1)]][1]) + \
      [(y,x,*rotate(dy,dx,l),cost-1000) for l in [0,1] if h.get((y,x,*rotate(dy,dx,l)),1e99) == cost-1000]
while len(q) > 0: [q.append(s) for s in mark(*q.pop(0))]
print(len(s)+1)