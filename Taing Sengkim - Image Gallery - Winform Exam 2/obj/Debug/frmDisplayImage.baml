   M S B A M L   `   `   `  ����  ImageGallery-WPF-Exam+    %ImageGallery_WPF_Exam.frmDisplayImage   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � WindowState$  	Maximized=�5   	    � Title$ Display Image��5   	   $	��450��6   $	��600��6,   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $��* �6   �� 5
      $
��Auto �6   .+�5   
   �� -   5         ��Name$ 	picHolder��5      $��1��6#   $��Uniform=�60   $��10q�6B   z�5      V� 5      .~�5      �� -   5        imageScaleTransform��5   %   T� 5      -   5        imageTranslateTransform��5   )   5      5      �� 5   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $��Bottom=�6X   $��10q�6s   .+�5      �� -   5         ��Name$ btnBack��5      $��80��6=   $��5q�6H   
������ $��0q�6l   .��5      �� $��images/backward.png��6   5      �� 5      -   5        $ 
btnZoomOut��5      $��0q�6)   
������ $��50��6o   $��5q�6z   .��5      �� $��images/zoomout.png��6   5      �� 5      -   5        $ 	btnZoomIn��5      $��50��6@   $��5q�6K   
������ $��0q�6o   .��5      �� $��images/zoomin.png��6   5      �� 5       -   5        $ btnNext��5       $��80��6=   $��5q�6H   
������ $��0q�6l   .��5!      �� $��images/forward.png��6   5"      5#      5$      5%      