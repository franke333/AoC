from math import prod; from time import sleep
inp = [[[int(a) for a in pv.split("=")[1].split(',')] for pv in l.split(' ')] for l in open("in/14.txt").readlines()]
qs = [0 for _ in range(4)]
inp100 = [[a+100*b for a,b in zip(p,v)] for p,v in inp]
[qs.__setitem__(q := (x%101 > 50) + 2* (y%103 > 51),qs[q] + 1) for x,y in inp100 if not ((x%101) == 50 or (y%103) == 51)]
print(prod(qs))
for i in range(89,100000,103): # i am literally him
      pic = set((x%101,y%103) for x,y in  [tuple(a+i*b for a,b in zip(p,v)) for p,v in inp])
      [print('#' if (x,y) in pic else '.',end=('' if x != 100 else '\n')) for y in range(103) for x in range(101)]
      print(f'\n---{i}---'); sleep(0.25)