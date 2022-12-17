class Jet:
    def Init():
        Jet.input = open("inputs/input17.txt").readline()
        Jet.pos = -1
    def GetDirection():
        Jet.pos += 1
        if Jet.pos == len(Jet.input):
            Jet.pos = 0
        return -1 if Jet.input[Jet.pos] == '<' else 1
        
class Piece:
    def __init__(self,data) -> None:
        self.data = data
        self.width = len(data[0])
        self.height = len(data)
        self.maxX=7-self.width
    def MoveH(self,x,y,level):
        dx = x + Jet.GetDirection()
        if dx < 0 or dx > self.maxX:
            return x,y
        for i in range(self.height):
            for j in range(self.width):
                if self.data[i][j] == 1 and level[y+i][dx+j] == 1:
                    return x,y
        return dx,y
    def MoveV(self,x,y,level):
        y -= 1
        if y < 0:
            return x,0,True
        for i in range(self.height):
            for j in range(self.width):
                if self.data[i][j] == 1 and level[y+i][x+j] == 1:
                    return x,y+1,True
        return x,y,False
    def Place(self,x,y,level):
        for i in range(self.height):
            for j in range(self.width):
                if self.data[i][j] == 1:
                    level[y+i][j+x] = 1
    def Spawn(self,yMostUpperRock):
        return 2,yMostUpperRock+4

def addLevel(level):
    level.append([0,0,0,0,0,0,0])

def simulatePiece(p,level,highestRock):
    x,y = p.Spawn(highestRock)
    while True:
        x,y = p.MoveH(x,y,level)
        x,y,res = p.MoveV(x,y,level)
        if res:
            highestRock = max(highestRock,y+p.height-1)
            p.Place(x,y,level)
            while(len(level) <= highestRock+7):
                addLevel(level)
            break
    return highestRock,x,y,

pieces = [
    Piece([[1,1,1,1]]),
    Piece([[0,1,0],[1,1,1],[0,1,0]]),
    Piece([[1,1,1],[0,0,1],[0,0,1]]),
    Piece([[1],[1],[1],[1]]),
    Piece([[1,1],[1,1]])
]

#part1
Jet.Init()
level = []
for _ in range(7):
    addLevel(level)
highestRock = -1
for i in range(2022):
    highestRock,_,_ = simulatePiece(pieces[i%5],level,highestRock)
print(highestRock+1)

#part2
Jet.Init()
level = []
for _ in range(7):
    addLevel(level)
dic = {}
highestRock = -1
inrow = 0
for i in range(3000):
    highestRock,x,y = simulatePiece(pieces[i%5],level,highestRock)
    if dic.get((x,Jet.pos,i%5)) == None:
        dic[(x,Jet.pos,i%5)] = (y,i,highestRock)
        inrow=0
    else:
        inrow+=1
        if inrow >= 5 and i%5==4:
            break
ly,li,lHighestRock = dic[(x,Jet.pos,i%5)]
cycleStart = li
cycleLength = i-li
cycleHeight = highestRock-lHighestRock
cyclesCount = (1000000000000 - cycleStart)//cycleLength
remainingPieces = 1000000000000 - cycleStart - cyclesCount*cycleLength
highestRockAtLastCycle = highestRock

for i in range(remainingPieces):
    highestRock,_,_ = simulatePiece(pieces[i%5],level,highestRock)
hightOfRemainingPieces = highestRock-highestRockAtLastCycle
print(lHighestRock+cyclesCount*cycleHeight+hightOfRemainingPieces)