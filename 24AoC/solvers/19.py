n,h = open("in/19.txt").read().split('\n\n')
start = lambda: None
def add_needle(needle, start):
    if not hasattr(start,'d'): start.d = {x:set() for x in ['w','u','b','g','r']}
    if len(needle) == 1: start.d[needle[0]].add(None); return
    o = lambda: None
    for i in start.d[needle[0]]: o = o if i is None else i
    start.d[needle[0]].add(o)
    add_needle(needle[1:], o)
[add_needle(needle, start) for needle in n.split(', ')]
def check(needle, p):
    if needle == '' or len(p) == 0: return sum(c for s,c in p if s==start or s==None)
    ps = sum([[*zip(s.d[needle[0]],[c,c])] for s,c in p],[])
    ps = [(s,c) if s != None else (start,c) for s,c in ps]
    d = {s:0 for s,_ in ps}
    for s,c in ps:  d[s] += c
    return check(needle[1:],d.items())
r = [check(needle, [(start,1)]) for needle in h.split('\n')]
print(sum(b>0 for b in r))
print(sum(r))