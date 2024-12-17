# Whispers of the Fountain AR
The augmented reality game that combines immersive storytelling & interactive experiences.

Creators: Alex Deaconu, Dina El-Kholy, Yehor Karpichev

The project also has a [VR implementation](https://github.com/YehorK/Whispers-of-the-Fountain-VR)! Check it out for a demo.

## Table of Contents
- [Project Setup](#project-setup)
- [Video Demo](#video-demo)
- [Storyline Overview](#storyline-overview)
- [Project Features](#project-features)
- [Acknowledgement](#acknowledgement)

## Project Setup
- You can simply git clone the main branch. The project is using Unity 2022.3.45f version.
- Due to the large file of the Vuforia configuration, make sure to download the .tgz file from [here](https://ubcca-my.sharepoint.com/:u:/r/personal/rishav_banerjee_ubc_ca/Documents/IMTC%20505%20(%2724)/ARcheologists/Whispers%20of%20the%20Fountain/AR%20app%20Vuforia%20tgz%20file/com.ptc.vuforia.engine-10.28.4.tgz?csf=1&web=1&e=hscZSZ), and place it in the Packages folder (next to the manifest) before opening the project to avoid any errors or mismatch in versions.
- Additionally, the build (.apk file) made for Android can be found [here](https://ubcca-my.sharepoint.com/:u:/r/personal/rishav_banerjee_ubc_ca/Documents/IMTC%20505%20(%2724)/ARcheologists/Whispers%20of%20the%20Fountain/the%20builds/Whispers%20of%20the%20Fountain%20AR.apk?csf=1&web=1&e=Pflano)

## Video demo:
[Watch the AR video](https://youtu.be/4WwmLCUnL6k?si=mAkPJzesjKFZvM4y)

## Storyline Overview
Our project takes players on an Augmented Reality treasure-hunting adventure through their campus, blending mystery, humor, and local Okanagan lore. The story begins with the protagonist discovering an ancient journal on the ground on campus (AR). The journal reveals the legend of Ogopogo, a spirit imprisoned beneath a fountain, whose freedom hinges on the retrieval of his scattered soul fragments. Guided by the journal, the player explores the UBCO Courtyard, finds fragments of Ogopogos soul, and even deals with creepy crawlies!

The journey culminates at the fountain, where the player reunites the fragments to free Ogopogo. The story ends with a twist as Ogopogo’s sarcastic remarks challenge the expectations of treasure and reward. With our project, we hope to showcase potential for immersive storytelling, combining exploration, problem-solving, and a humor-driven narrative to create a captivating player experience. 

## Project Features

Target Image 1: Journal

Marker Location: Near the fountain. 

Upon scanning, a journal appears over the image target, displaying a text  
"Huh? What's this journal doing on the ground? Try tapping the journal". When the user taps on the journal, the introductory sound clip begins playing, providing guidance for the player on how the game goes. The user is informed that they need to find crystals scattered around the courtyard. The player should get the hint to find the first crystal near the fountain. 
 

Target Image 2: First Soul Fragment 

Marker Location: Base of the fountain waterfall. 

Upon scanning, a crystal pops out of the image target, and the player is supposed to tap on it for collection. After collecting the crystal, the journal appears once again and narrates another script that hints the user to go to the next crystal location. 
 

Target Image 3: Second Soul Fragment 

Marker location: On one of the deer statues in the courtyard. 

Upon scanning, the same behavior as the first soul fragment is repeated. After tapping on the fragment, the journal appears and again narrates another script hinting for the last soul fragment. 
 

Target Image 4: Third Soul Fragment 

Marker Location: On the tent poles seen in UBCO’s courtyard.  

Upon scanning, a crystal appears, however, this time a spider is on top of it. A text prompt is given to the player, asking them to shake their phone in order to get rid of the spider. After shaking, the spider is gone, and the player is able to collect the soul fragment as before. Upon collection, the journal appears and narrates a “well done” script, signifying that the player has collected all Ogopogo's soul fragments. The sound clip also asks the player to return to the fountain.  
 

Target Image 5: Ogopogo  

Marker Location: The front of the fountain.  

Upon scanning, the journal appears congratulating the player on their dedication in collecting the fragments and asks them to toss them all in the fountain to free Ogopogo. The three fragments then appear, each in a different color, and the player is supposed to tap on each fragment to proceed. After tossing all the fragments, the journal disappears and Ogopogo, a snake, appears and starts narrating the closing script. When the narration is done, Ogopogo fades out from the scene. This was done by fading the snake GameObject material colors. 


## Acknowledgement
Spider by Quaternius: https://poly.pizza/m/yRYJiAJyiM

Crystal Asset by iPoly3D: https://poly.pizza/m/7Do2pm4F8O 

Journal models by Quaternius: https://poly.pizza/m/h3Wh4fxSQX https://poly.pizza/m/JO0bV6osMQ

Vuforia Image Targets generated with DallE-3 in ChatGPT.


