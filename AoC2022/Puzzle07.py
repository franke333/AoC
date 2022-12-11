class directory:
    def __init__(self,parent) -> None:
        self.subdirectories = {}
        self.fileSum = 0
        self.size = None
        self.parent = parent
    def GetSize(self):
        if self.size == None:
            self.size = sum(x.GetSize() for x in self.subdirectories.values()) + self.fileSum
        return self.size
    def AddFile(self,size):
        self.fileSum += size
    def AddDirectory(self,name):
        self.subdirectories[name] = directory(self)
    def GetDirectory(self,name):
        return self.subdirectories[name]

mainDir = directory(None)
currentDir = mainDir
for l in open("inputs/input07.txt").readlines():
    params = l[:-1].split()
    match params:
        case ['$','cd','/']:
            currentDir = mainDir
        case ['$','cd','..']:
            currentDir = currentDir.parent
        case ['$','cd',x]:
            currentDir = currentDir.GetDirectory(x)
        case ['dir',x]:
            currentDir.AddDirectory(x)
        case ['$','ls']:
            pass
        case [size,_]:
            currentDir.AddFile(int(size))

#part1
dirs = {mainDir}
mem = 0
while(len(dirs)!=0):
    currentDir = dirs.pop()
    if currentDir.GetSize() <= 100_000:
        mem += currentDir.GetSize()
    dirs.update(currentDir.subdirectories.values())
    
#part2
needed_mem = 30_000_000 - (70_000_000 - mainDir.GetSize())
min_size_over_need = mainDir.GetSize()
dirs = {mainDir}
while(len(dirs)!=0):
    direc = dirs.pop()
    if direc.GetSize() >= needed_mem and direc.GetSize() < min_size_over_need:
        min_size_over_need = direc.GetSize()
    dirs.update(direc.subdirectories.values())

print(mem,min_size_over_need)