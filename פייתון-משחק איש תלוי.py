import random

HANGMANPICS = ['''

  +---+
  |   |
      |
      |
      |
      |
=========''', '''

  +---+
  |   |
  O   |
      |
      |
      |
=========''', '''

  +---+
  |   |
  O   |
  |   |
      |
      |
=========''', '''

  +---+
  |   |
  O   |
 /|   |
      |
      |
=========''', '''

  +---+
  |   |
  O   |
 /|\  |
      |
      |
=========''', '''

  +---+
  |   |
  O   |
 /|\  |
 /    |
      |
=========''', '''

  +---+
  |   |
  O   |
 /|\  |
 / \  |
      |
=========''']

# ======אוסף מילים=====
wordsList = ["hello", "cute", "telephone", "beautiful", "pizza","love","wonderful","Israel"]

# ============פונקציות=============
# פונקציה של לוח המשחק
def displayBoard(HANGMANPICS, missedLetters, correctLetters, secretWord):
    print(HANGMANPICS[len(missedLetters)])
    print("Missed Letters: " + ", ".join(missedLetters))
    an = ''
    for s in secretWord:
        if s in correctLetters:
            an += s
        else:
            an += '_'
    print(an)

# פונקציה שבודקת תקינות של תו
def getGuess(alreadyGuessed):
    flag = False
    g = input("Guess a Letter:")
    while not flag:
        if len(g) != 1: print("You need to guess only one character, please try again!")
        elif g in alreadyGuessed:print("You have already guessed this letter, please try another letter ")
        else:
            return g
        g = input()

# פונקציה שבודקת האם השחקן רוצה משחק חדש או לא
def playAgain():
    return input("Do you want to play again? (enter y/n)").lower() == 'y'

# ======== תחילת המשחק================
missedLetters = ''
correctLetters = ''
gameIsDone = False
secretWord = random.choice(wordsList)
print(secretWord)

while not gameIsDone:
    displayBoard(HANGMANPICS, missedLetters, correctLetters, secretWord)

    # המשתמש מקיש אות ובודקים אם זה תקין
    guess = getGuess(missedLetters + correctLetters)

    if guess in secretWord:
        correctLetters += guess
        # בדיקה אם השחקן ניצח
        foundAllLetters = True
        for l in range(len(secretWord)):
            if secretWord[l] not in correctLetters:
                foundAllLetters = False
                break
        if foundAllLetters:
            print('Yes! The secret word is "' + secretWord + '"! You have won!')
            gameIsDone = True
    else:
        missedLetters = missedLetters + guess

        # בדיקה האם השחקן הפסיד
        if len(missedLetters) == len(HANGMANPICS) - 1:
            displayBoard(HANGMANPICS, missedLetters, correctLetters, secretWord)
            print('You have run out of guesses!\nAfter ' + str(len(missedLetters)) + ' missed guesses and ' + str(
                len(correctLetters)) + ' correct guesses, the word was "' + secretWord + '"')
            gameIsDone = True

    # האם השחקן רוצה לשחק שוב?? 
    # אתחול המשתנים והמשחק מתחדש...
    if gameIsDone:
        if playAgain():
            missedLetters = ''
            correctLetters = ''
            gameIsDone = False
            gameIsDone = False
            secretWord = random.choice(wordsList)
        else:
            break
