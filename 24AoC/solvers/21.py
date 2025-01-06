alphabet = '^>v<A'
pos = {
    'A': (0,2),
    '^': (0,1),
    'v': (1,1),
    '<': (1,0),
    '>': (1,2),
    'X': (0,0),
}
posn = {
    'A': (3,2),
    '0': (3,1),
    '1': (2,0),
    '2': (2,1),
    '3': (2,2),
    '4': (1,0),
    '5': (1,1),
    '6': (1,2),
    '7': (0,0),
    '8': (0,1),
    '9': (0,2),
    'X': (3,0),
}

def getPossiblePaths(start,end,display):
    delta = (display[end][i] - display[start][i] for i in range(3))