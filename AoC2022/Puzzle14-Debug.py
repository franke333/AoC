import numpy as np

minx,maxx,maxy = 500,500,0
inp = []
for l in open("inputs/input14.txt"):
    line = []
    for p in l.split(' -> '):
        x,y = p.split(',')
        minx = min(minx,int(x))
        maxx = max(maxx,int(x))
        maxy = max(maxy,int(y))
        line.append([int(x),int(y)])
    inp.append(line)


def printWorld(world):
    for line in world:
        for x in line:
            match x:
                case 0:
                    print('.',end="")
                case 1:
                    print('#',end="")
                case 2:
                    print('o',end="")
                case y:
                    print(y,end="")
        print()
maxy += 1
minx -= 1
maxx += 1
world = []
for _ in range(maxy+1):
    world.append([0] * (maxx - minx + 1)) 
world[0][500-minx] = '+'
for l in inp:
    prev = l[0]
    for p in l[1:]:
        direction = np.sign(np.subtract(p,prev))
        while (np.array(prev) != np.array(p)).any():
            world[prev[1]][prev[0]-minx] = 1
            prev = np.add(prev,direction)
        world[prev[1]][prev[0]-minx] = 1
#part 1 
sand = 0
while True:
    y,x = (1,500-minx)
    while y < maxy and x < maxx-minx and x > 0:
        if world[y+1][x] == 0:
            y+=1
        elif world[y+1][x-1] == 0:
            x-=1
            y+=1
        elif world[y+1][x+1] == 0:
            x+=1
            y+=1
        else:
            world[y][x] = 2
            break
    if not(y < maxy and x < maxx-minx and x > 0):
        break
    sand+=1
printWorld(world)
print('--------------')
print(sand)

#part 2:

world.append([1] * (maxx - minx + 1))
while True:
    y,x = (0,500-minx)
    while True:
        if world[y+1][x] == 0:
            y+=1
        elif x > 0 and world[y+1][x-1] == 0:
            x-=1
            y+=1
        elif x < maxx-minx and world[y+1][x+1] == 0:
            x+=1
            y+=1
        else:
            world[y][x] = 2
            break
    sand+=1
    if sand%1_000_000 == 0:
        printWorld(world)
        print('--------------')
    if (y,x) == (0,500-minx):
        break
printWorld(world)
for x in [0,maxx-minx]:
    for y in range(maxy+2):
        if world[y][x]==2:
            break
    sand += (((maxy-y)**2)+(maxy-y)) /2
print(int(sand))