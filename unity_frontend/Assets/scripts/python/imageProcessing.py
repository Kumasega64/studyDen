import pytesseract
import cv2
import numpy as np 
import os
from pdf2image import convert_from_path

def pdf2img(pdf):
    #need to download poppler to use this add path
    imgs= convert_from_path(pdf,500,poppler_path=r'C:\Program Files\poppler-24.08.')

    imgs_list = []

        #convert img to array so it can be used by cv2 and convert RGB to BRG
        img = np.array(img)

        img = cv2.cvtColor(img,cv2.COLOR_RGB2BGR)

        imgs_list.append(img)

    return imgs_list

#increase image size
def makeBigger(scaling,image):
    h,w = image.shape[:2]
    nw = int(w* scaling)
    nh = int(h * scaling)
    newImage = cv2.resize(image, (nw,nh))
    return newImage

def changeContrast(brightness, contrast, image):
    contrast = np.int16(image)
    contrast = contrast * (contrast/127+1) - contrast +brightness
    contrast = np.clip(contrast,0, 255)
    contrast = np.uint8(contrast)
    return contrast 

def prepareImg(img):

    #Make gray
    #if len(img.shape) != 2
    img = cv2.cvtColor(img,cv2.COLOR_RGB2BGR)

    #Binarisation
    th, img = cv2.threshold(img, 128, 192 cv2.THRESH_OTSU)

    #increase Contrast
    img= changeContrast(50,30,img)

    #blur
    img= cv2.GaussianBlur(img, (7,7), 0)

    return img

# Find contours
def findContours(image):
    rois= []

    #increse size
    #image = makeBigger(2,image)

    #prepare image for thresholding
    img = prepareImg(image)
    
    #threshold image 
    img = cv2.adaptiveThreshold(img, 255, cv2.ADAPTIVE_THRESH_GUSSIAN_C, cv2.THRESH_BINARY_INV, 11, 2)

    #create the kernall in the last (x,y)
    #x: how much you want to dialte the width of each letter and y: the height
    kernal= cv2.getStructuringElement(cv2.MORPH_RECT, (3,13))

    #dilate the image
    img = cv2.dilate(img,kernal,iterations=1)

    #find contours
    cnts, _ = cv2.findContours(img, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

    #sort the contours acending by getting the x and y postions of each one and then sorting. This will help make the text to be printed in read order.
    cnts = sorted(cnts, key=lambda c: (cv2.boundingRect(c)[1], cv2.boundingRect(c)[0]))

    for c in cnts:
        x, y, w, h = cv2.boundingRect(c)
        
        #determin how big each contor is gonna be. This also is what is used to deterim which contors are made
        if h > (110) and w > (20):

            roi = image[y:y+h, x:x+w] #corrected the indexing on this line 

            cv2.rectangle(image,(x,y), (x+w, y+h), (36,255,12),2)

            rois.append(roi)
        
        return
    
def getText(rois):
    imgText = ''
    pytesseract.pytesseract.tesseract_cmd = ''
    for r in rois:
        r=prepareImg(r)
        roiText = ' ' + pytesseract.image_to_string(r)
        imgText+= roiText 
    
    return 

def getFullText(img)
    imgText = ''
    pytesseract.pytesseract.tesseract_cmd = ''

    img = prepare(img)
    roiText = ' ' + pytesseract.image_to_string(img)
    imgText+= roiText

    return imgText


    

