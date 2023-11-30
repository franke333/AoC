part2 = True

height_map,visits = [],[]
h = -1
frontier = list()
for l in open("inputs/input12.txt"):
    w=0
    h+=1
    line_h,line_v = [],[]
    for c in l[:-1]:
        match ord(c):
            case 83:
                frontier.append((h,w))
                line_h.append(ord('a'))
                line_v.append(0)
            case 69:
                line_h.append(ord('z'))
                line_v.append(-2)
                print(h,w)
            case x:
                line_h.append(x)
                if part2 and x == ord('a'):
                    frontier.append((h,w))
                line_v.append(-1)
        w+=1
    height_map.append(line_h)
    visits.append(line_v)

directions = [(-1,0),(1,0),(0,1),(0,-1)]
w-=1
step = 1
while len(frontier) > 0:
    next_frontier = list()
    for x,y in frontier:
        for dx,dy in directions:
            if x+dx < 0 or x+dx > h or y+dy < 0 or y+dy > w:
                continue
            if height_map[x+dx][y+dy] <= height_map[x][y] + 1:
                if visits[x+dx][y+dy] < 0:
                    if visits[x+dx][y+dy] == -2:
                        print(step)
                    visits[x+dx][y+dy] = step
                    next_frontier.append((x+dx,y+dy))
    frontier = next_frontier
    step += 1