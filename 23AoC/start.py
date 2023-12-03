import os
import datetime

source_folder = 'Puzzles' 
input_folder = 'Inputs'
header_file = 'AllIncludes.h'
file_name = 'Puzzles/Puzzle{}.cpp'

if(len(os.sys.argv) < 2):
    day = datetime.datetime.now().day
else:
    day = int(os.sys.argv[1])

if(day < 1 or day > 25):
    print('Please provide a valid day')
    exit(1)

# check if the file already exists
if(os.path.exists(file_name.format(day))):
    print(f'File {file_name.format(day)} already exists')
    exit(1)

# create the file
print(f'Including day {day}')

with open(file_name.format(day), 'w') as f:
    f.write('#include "../Utility.h"\n#include "../Puzzle.h"\n\n')
    f.write(f'class Puzzle{day} : IPuzzle {{\n')
    f.write('\tvoid Solve() override\n\t{\n\n\t}\n')
    f.write('\tvoid SolveAdvanced() override\n\t{\n\n\t}\n')
    f.write('};\n')

# update the header file
print('Updating header file')

with open(header_file, 'w') as f:
    f.write('#pragma once\n\n')
    for filename in os.listdir(source_folder):
        if filename.endswith('.cpp'):
            f.write(f'#include "{source_folder}/{filename}"\n')

# create the input file
print('Creating input file')

with open(f'{input_folder}/Input{day}.txt', 'w') as f:
    f.write('')
