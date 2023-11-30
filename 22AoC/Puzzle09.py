import numpy as np

def Move(H,T):
    if max(abs(np.subtract(H,T))) <= 1:
        return None
    direction = tuple(np.sign(np.subtract(H,T)))
    path = tuple(np.add(T,direction))
    return path

for part in [2,10]:
    knots = part*[(0,0)]
    visited = {knots[0]}
    for move in open("inputs/input09.txt"):
        match move.split():
            case 'R',x:
                d = (0,1)
            case 'L',x:
                d = (0,-1)
            case 'U',x:
                d = (1,0)
            case 'D',x:
                d = (-1,0)
        for _ in range(int(x)):
            knots[0] = tuple(np.add(knots[0],d))
            for i in range(part-1):
                path = Move(knots[i],knots[i+1])
                knots[i+1] = path if path else knots[i+1]
            if path:
                visited.add(path) #last path 
    print(len(visited))