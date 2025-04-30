from imageProcessing import getFullText, getText
import cv2


img_path = r"C:\Users\kuma\Downloads\PXL_20250426_223810438.RAW-01.COVER.jpg"

img = cv2.imread(img_path)

print('test')
text = getFullText(img)

print(text)