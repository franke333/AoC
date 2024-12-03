from functools import reduce; import re
inp = open('in/03.txt').read()
e = lambda y: int(y[0][0][4:])*int(y[0][1][:-1])
f = lambda y,s: max([*filter(lambda x: x < y[1], [m.end() for m in re.finditer(s,inp)])]+[0])
m = [(m.group().split(','),m.start()) for m in re.finditer('mul\\([0-9]{0,3},[0-9]{0,3}\\)', inp)]
print(reduce(lambda x,y: x+e(y), m, 0))
print(reduce(lambda x,y: x+(e(y) if (f(y,"do()")>=f(y,"don't()")) else 0), m, 0))