i = open("inputs/input04.txt").readlines()
m = list(map(lambda s: [int(a) for a in s.replace(',','-').split('-')],i))
print(sum([(a >= c and b <= d) or (a <= c and b >= d) for a,b,c,d in m]))
print(sum([(a <= c and c <= b) or (a >= c and d >= a) for a,b,c,d in m]))