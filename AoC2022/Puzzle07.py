from hashlib import new


class directory:
    def __init__(self) -> None:
        self.subdirectories = {}
        self.fileSum = 0
        self.size = None
    def GetSize(self):
        if self.size == None:
            self.size = sum(x.GetSize() for x in self.subdirectories) + self.fileSum
        return self.size
    def AddFile(self,size):
        self.fileSum += size
    def AddDirectory(self,name):
        self.subdirectories[name] = directory()
    def GetDirectory(self,name):
        return self.subdirectories[name]

mainDIrectory
for 

