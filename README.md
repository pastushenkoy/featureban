# Featureban

This repository contains final project completing my education in DodoDevSchool. Featureban is a game, simulating kanban process. The purpose of this project is to create domain models for Featureban using TDD and DDD practices. 

Simulation contains two domains:
1. Game. This domain exports only one class, which embodies one simulation game. Game includes board and players. Game has two public methods which allows you two spend one or many rounds of the game.
2. Statistics. This domain aggregates one or many games and calculates so called Throughput. This value is an average value of numbers of done cards on board after certain number of rounds (days).

[More about Featureban.](https://static1.squarespace.com/static/53c5a5bce4b095e6864c0a80/t/5a83c6b771c10b85cc29a568/1518585573238/featureban-slides-2.2.pdf)
