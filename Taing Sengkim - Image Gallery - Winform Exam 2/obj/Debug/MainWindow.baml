   M S B A M L   `   `   `  ����P  LImageGallery-WPF-Exam, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:ImageGallery_WPF_ExamImageGallery_WPF_Exam  5   	   &     ImageGallery_WPF_Exam.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:ImageGallery_WPF_Exam     5   	     � WindowState$  	Maximized=�5   	   
������� � Icon$ images/3DImage.png��5
   	    � Title$ Image Gallery��5   	   $	��650��6   $
��1000��6,    � 	Resources 5         IconsPanelTemplate � Orientation  ListPanelTemplate �� Orientation��%�   &	        &	 L     �� 5   
   .R�5      � $��20q�6   $ 
Horizontal=�6$   5      �� 5   
   .R�5      �� $��20q�6   $ Vertical=�6%   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $
��0.2* �6   �� 5      $��* �6   .+�5"   
   � $��0��6    5#      �� 5$      $
��0.1* �6    �� 5%      $��* �6    .+�5'      � $��0��6   .+�5)      �� $ 
Horizontal=�6   $��Center=�66   .+�5*      �� -   5        $��btnBack��5*   1   $��0q�6   
������ $��50��6Y   $��50��6d   $
��10,0q�6p   .��5+      �� $��images/backward.png��6    5,      �� 5-      -   5        $��
btnForward��5-   J   $��0q�6   
������ $��50��6]   $��50��6h   $	��0,0q�6t   .��5.      �� $��images/forward.png��6    5/      �� 50      -   5        $��cmbViewMode��50      $	��100��63   $��30��6?   $
��10,0q�6K    �� Text$ 	View Mode��6Y   $��Right=�6j   .@�53      �� $��Large Icons��6'   �� 54      $��Medium Icons��6'   �� 55      $��Small Icons��6'   �� 56      $
��List��6'   57      58      59      � 5:      $��1��6   .+�5;      R� -   5        $��treeViewFileSystem��5;      $��15(�65   
�������
�������5<      5=      5?      � 5@   
   $��1��6   $��1��6   .+�5A       � -   5        $��displayImage��5A      	  � JournalOwnership$	 OwnsJournal=�6(   
  � NavigationUIVisibility$
 Hidden=�6G   5B      5C      5D      