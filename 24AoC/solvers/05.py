from functools import reduce as red
inp = open("in/05.txt").readlines()
r,p = [[*map(int,x.split('|'))] for x in inp[:inp.index('\n')]],[[*map(int,x.split(','))] for x in inp[inp.index('\n')+1:]]
ok = lambda u: all([(all(y not in u[:i] for x,y in r if x==n)) for i,n in enumerate(u)])
print(sum([u[len(u)//2] for u in p if ok(u)]))
print(sum([red(lambda a,x: [a[:i] + [x] + a[i:] for i in range(len(a)+1) if all(ok([x]+[w]) for w in a[i:])][0],u,[])[len(u)//2] for u in p if not ok(u)]))