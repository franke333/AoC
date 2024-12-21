from copy import deepcopy;
inp,c = open('in/15.txt').read().split('\n\n')
m = [[b for b in a] for a in inp.split('\n')]
ag = deepcopy(ags := [[x,y] for y in range(len(m)) for x in range(len(m[y])) if m[y][x] == '@'][0])
c = [['^','>','v','<'].index(n) for n in c.replace('\n','')]
dirs = [(0,-1),(1,0),(0,1),(-1,0)]
move = lambda x,y,dx,dy,n: False if m[y][x] == '#' else (m[y][x] == '.' and (m[y].__setitem__(x,n) == None)) or (move(x+dx,y+dy,dx,dy,m[y][x]) and m[y].__setitem__(x,n) == None)
agMove = lambda d: m[ag[1]].__setitem__(ag[0],'.') or ag.__setitem__(0,ag[0]+dirs[d][0]) or ag.__setitem__(1,ag[1]+dirs[d][1]) 
[move(*ag,*(dirs[d]),m[ag[1]][ag[0]]) and agMove(d) for d in c]
print(sum(sum([[100*y+x for x,n in enumerate(l) if n=='O'] for y,l in enumerate(m)],[])))

ag = [ags[0]*2,ags[1]]
mm = [sum([({'#': ['#','#'], '.': ['.', '.'], 'O': ['[', ']'], '@': ['@', '.']}[b]) for b in a],[]) for a in inp.split('\n')]
offsets = lambda c: [1,0] if c == '[' else [-1,0]
moveH = lambda x,y,dx,dy,n: False if mm[y][x] == '#' else (mm[y][x] == '.' and (mm[y].__setitem__(x,n) == None)) or (moveH(x+dx,y,dx,0,mm[y][x]) and mm[y].__setitem__(x,n) == None)
moveV = lambda x,y,dx,dy,n,mm: False if mm[y][x] == '#' else (mm[y][x] == '.' and (mm[y].__setitem__(x,n) == None)) or \
      all((moveV(x+o,y+dy,0,dy,mm[y][x+o],mm) and (mm[y].__setitem__(x+o,n if o == 0 else '.') == None)) for o in offsets(mm[y][x]))
mmove = lambda x,y,dx,dy,n: moveH(x,y,dx,dy,n) if dx != 0 else (moveV(x+dx,y+dy,dx,dy,n,mc := deepcopy(mm)) and setmm(mc))
setmm = lambda mc: [mm.__setitem__(y,mc[y]) for y in range(len(mc))]
agMMove = lambda d: mm[ag[1]].__setitem__(ag[0],'.') or ag.__setitem__(0,ag[0]+dirs[d][0]) or ag.__setitem__(1,ag[1]+dirs[d][1]) or True
[mmove(*ag,*(dirs[d]),'@') and agMMove(d) for d in c]
print(sum(sum([[100*y+x for x,n in enumerate(l) if n=='['] for y,l in enumerate(mm)],[])))