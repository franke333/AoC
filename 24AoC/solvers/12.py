inp = open("in/12.txt").read().strip().split("\n")
checked = [[False for _ in range(len(inp))] for _ in range(len(inp))]
ok = lambda x: all(0 <= y < len(inp) for y in x)
dirs = [(0,1),(-1,0),(0,-1),(1,0)]
perim = lambda x,y: sum([1 for dx,dy in dirs if ok((x+dx,y+dy)) and inp[x+dx][y+dy] == inp[x][y]],0)
check = lambda x,y,checked: checked[x].__setitem__(y,True)
fence = lambda x,y,c,checked: [sum(x) for x in zip((1,4-perim(x,y)),*[fence(x+dx,y+dy,c,checked) for dx,dy in dirs])] if \
    ok((x,y)) and c==inp[x][y]  and not checked[x][y] and not check(x,y,checked) else (0,0)
print(sum(a*b for a,b in [fence(x,y,inp[x][y],checked) for x in range(len(inp)) for y in range(len(inp))]))

edged = set()
def sides(sx,sy,sdi=0):
    c,x,y,di,edges = inp[sx][sy],sx,sy,sdi,0
    while True:
        next = (x+dirs[di][0],y+dirs[di][1])
        nextUp = (x+dirs[(di+1)%4][0]+dirs[di][0],y+dirs[(di+1)%4][1]+dirs[di][1])
        if not ok(next) or inp[next[0]][next[1]] != c: edges += 1; di = (di+3)%4
        elif ok(nextUp) and inp[nextUp[0]][nextUp[1]] == c:  edges += 1; di = (di+1)%4; x,y = nextUp
        else: x,y = next
        edged.add((x,y,di))
        if (x,y,di) == (sx,sy,sdi): break
    return edges
checked = [[False for _ in range(len(inp))] for _ in range(len(inp))]
s = 0
for x in range(len(inp)):
    for y in range(len(inp)):
        if not checked[x][y]:
            if ok((x-1,y)) and (x-1,y,2) not in edged:
                s += sides(x-1,y,2)*fence(x-1,y,inp[x-1][y],[[False for _ in range(len(inp))] for _ in range(len(inp))])[0]
            s += sides(x,y)*fence(x,y,inp[x][y],checked)[0]
print(s)