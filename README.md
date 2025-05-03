# **Study Buddy:**

Study Buddy is a mobile application that turns studying into an interactive and rewarding experience. Users create their own personalized "Study Buddy" avatar and level it up by engaging with educational content and mini-games.



## **FEATURES:**

  ### Create and Customize Avatars:
  
  Users can create a unique Study Buddy avatar, assign it a name, and level it up with experience points earned through studying.

  ### Scan and Store Books:
  
  Take pictures of multiple pages from a physical book using your phone's camera. Name and save the digital version of your book directly in the app.

  ### Play Mini-Games:
  
  Engage with games based on the content of your saved books. These games help reinforce knowledge and reward you with XP for your avatar.

  ### AI-Generated Questions:
  
  Questions and answers for the mini-games are generated using OpenAI's ChatGPT API. We extract text using Tesseract OCR from scanned book pages and feed it into the API.

  ### Local Data Storage:
  
  All user-specific data, including avatar details (name, species, level, XP) and book content (names, questions, answers), is stored securely using an SQLite database on the device.

  ### Unity-Powered UI:
  
  The application front-end is built with Unity, delivering a rich and responsive user experience across mobile platforms.



## **TECH STACK:**

  ### Frontend: 
  Unity Game Engine (C#)

  ### Backend: 
  SQLite for local storage

  ### OCR: 
  Python pytesseract for text extraction from images

  ### AI Integration: 
  ChatGPT via OpenAI API for question generation

  ### Platform: 
  Mobile (Android/iOS)
