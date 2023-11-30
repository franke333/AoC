import os

source_folder = 'Puzzles' 
header_file = 'AllIncludes.h'

with open(header_file, 'w') as f:
    f.write('#pragma once\n\n')
    for filename in os.listdir(source_folder):
        if filename.endswith('.h'):
            f.write(f'#include "{source_folder}/{filename}"\n')