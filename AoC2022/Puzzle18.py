import numpy as np
 #part 1
world = np.zeros((24,24,24)).astype(int)
surface = 0
for l in open("inputs/input18.txt"):
    x,y,z = [int(a) for a in l.split(',')]
    if world[x][y][z]:
        print("DUPLICATE")
    for i in [-1,1]:
        surface += -1 if world[x+i][y][z] else 1
        surface += -1 if world[x][y+i][z] else 1
        surface += -1 if world[x][y][z+i] else 1
    world[x][y][z] = 1
print(surface)

#part 2   (data loaded in part 1)
surface = 0
counted = set()
queue = list()
queue.append((0,0,0))
for i in range(1,24):
    for x in [(0,0,i),(0,i,0),(i,0,0)]:
        queue.append(x)
        counted.add(x)
    for j in range(1,24):
        for x in [(0,i,j),(i,j,0),(i,0,j)]:
            queue.append(x)
            counted.add(x)
while len(queue) > 0:
    a,b,c = queue.pop()
    if world[a][b][c]:
        for x in [a,b,c]:
            if x == 0:
                surface += 1
        continue
    for i in [-1,1]:
        for x,y,z in [(a+i,b,c),(a,b+i,c),(a,b,c+i)]:
            if x >= 0 and x <= 23 and y >= 0 and y <= 23 and z >= 0 and z <= 23:
                if world[x][y][z]:
                    surface += 1
                else:
                    if (x,y,z) not in counted:
                        counted.add((x,y,z))
                        queue.append((x,y,z))
print(surface)