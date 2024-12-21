for i in range(1024,3450):
    inp = set((int((x := l.split(','))[0]),int(x[1])) for l in open('in/18.txt').readlines()[:i])
    ok = lambda x,y: 0 <= x < 71 and 0 <= y < 71 and (x,y) not in inp
    s = [(0,0,0)]
    sc = set()
    while True:
        if(len(s) == 0): print("part2: "+open('in/18.txt').readlines()[i-1]); exit()
        x,y,d = s[0]
        s = s[1:]
        if (x,y) in sc: continue
        sc.add((x,y))
        if (x,y) == (70,70): break
        [s.append((x+dx,y+dy,d+1)) for dx,dy in ((0,1),(0,-1),(1,0),(-1,0)) if ok(x+dx,y+dy)]
    if i == 1024: print("part1: "+str(d))