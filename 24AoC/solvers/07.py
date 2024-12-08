import operator as op; import itertools as it; import functools as ft
inp = [(int(p),[*map(int,q.split(' '))]) for p,q in [l.split(': ') for l in open("in/07.txt").readlines()]]
cal = lambda ops : sum([p for p,q in inp if \
    any([p == ft.reduce(lambda a,b : b[1](a,b[0]),[*zip(q[1:],c)],q[0]) for c in it.product(ops,repeat=len(q)-1)])])
print(cal([op.mul,op.add]))
print(cal([op.mul,op.add,lambda a,b : int(f"{a}{b}")]))