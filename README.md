# Cruxlab.Tournament.2022.DotNet

Let`s say we have a file with password and their validity requirement. Its looks like:
```
a 1-5 abcdj
z 2-4 asfalseiruqwo
b 3-6 bhhkkbbjjjb
```
Each row consists of password requirements and password itself. Requirement source is a symbol which should be present in password and its appearance count range.

In an example above two passwords are valid `cause corresponding symbols appear on passwords itself; their appearance count matches count range requirement.

So you should write a code which counts valid passwords at file... and writes it to the console.
