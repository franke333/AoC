# 0 = rock, 1 = paper, 2 = scissors
def ResultScore(A,B):
    return (6 if B == (A+1)%3 else 3 if A == B else 0)
    
F = open("inputs/input02.txt")
score1,score2 = 0,0
for line in F:
    A,B = ord(line[0])-ord('A'),ord(line[2])-ord('X')
    score1 += ResultScore(A,B) + (B+1)
    score2 += B*3 + ((A+B-1)%3 + 1)
print(score1,score2)