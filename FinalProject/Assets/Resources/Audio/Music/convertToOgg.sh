#!/bin/bash

# Loop through all .mp3 files in the directory
for file in *.mp3; do
  # Get the filename without the extension
  filename="${file%.*}"
  
  # Convert to OGG format using ffmpeg
  ffmpeg -i "$file" -vn -acodec libvorbis "$filename.ogg"

  echo "Converted $file to $filename.ogg"
done

