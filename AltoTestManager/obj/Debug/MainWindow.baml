   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	   J FAltoTestManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:AltoTestManager.ConvertersAltoTestManager.Converters 5   	   1clr-namespace:AltoTestManagerAltoTestManager 5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5   	       AltoTestManager.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   9conv(clr-namespace:AltoTestManager.Converters     5   	   /localclr-namespace:AltoTestManager     5   	   _iePclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5   	   -   5          �Name$  wm��5   	    � Title$ Test Manager��5   	   $	��600��6   $	��700��6+    � Icon$ img/favicon.ico��5   e   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Closing��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6%   �� *CommandSaveJson+5       � 	Resources 5      7 1AltoTestManager.SelectedProjectToEnabledConverter %  !SelectedProjectToEnabledConverter( "AltoTestManager.ImageNullConverter  ImageNullConverter2  ,AltoTestManager.StringToImageSourceConverter   StringToImageSourceConverterG AAltoTestManager.Converters.NotificationTypeToBorderColorConverter * &NotificationTypeToBorderColorConverterE	 ?AltoTestManager.Converters.IsModeUpdateToButtonContentConverter ( $IsModeUpdateToButtonContentConverter5
 /AltoTestManager.Converters.ObjectArrayConverter  ObjectArrayConverter��%�   &	        &	      &	 &     &	 9     &	 L     &	 _       5   
   62     5   
   6#     5   
   6-     5   
   66   	  5   
   64   
  5   
   6$   5       J� InputBindings 5      �� 5   
   	 �� 	Modifiers$	 Controlf�6   

 �� Key$
 V��6)    �� Command 61   �� *CommandGetImageFromClipboard+ �� CommandParameter 6b   Z� 5      .��5      �� .@�5   
   �� $��Test Senaryoları��6   .��5      �  � ColumnDefinitions 5      �� 5       5"      �� 5#      6)   .+�5&      �  � RowDefinitions 5'      �� 5(      $
��Auto �6(   �� 5)      $
��Auto �6(   �� 5*      $��* �6(   �� 5+      $
��Auto �6(   �� 5,      $
��Auto �6(   .+�5.      �  5/      �� 50      �� 51      �� 52      .+�54      ]� $��0,4,0,0q�6$   .+�55      	� $
��Test��6_   $��Center=�6n   $��20��6�   $��80��6�    d� 	IsChecked 6*   �� *IsTestEnvironment+ �� Mode$ TwoWay=�6�   	� 56      $��Preprod��6b   $��Center=�6t   $��20��6�   $��80��6�    6*   �� *IsPreprodEnvironment+$ TwoWay=�6�   �� 57      $	��140��6%   $��Right=�61   $��Right=�58   N   $��25��6e   ��57   M   �� *CommandCloneTestProject+��58   !   �� *SelectedProject+��6q   �� *CloneButtonContent+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5:      ]� 5;      $��1��6$   ��. .+�5<      �� $��Right=�5=   N   $��25��6e   $��Worde Çıkart��6q   ��5<   %   �� *CommandExportTestProjectWord+��5=   !   �� *SelectedProject+6q   �� 5>      $��1��6%    �� Padding$ 2q�62   $��5q�6>   $��Stretch=�6I   $��Center=�6g   
���   �$��1q�6�   ._�5?   "   ��  �� StaysOpenOnEdit .   �� IsTextSearchCaseSensitive .   �� IsTextSearchEnabled .  �� 
IsEditable .  w� TextPath$ Caption��6�    �� DisplayMemberPath$ Caption��64   �� SelectedItem 6�   �� *SelectedProject+$ TwoWay=�$ PropertyChanged=���6
  �� *TestProjectsByEnv+5@   #   5A      5B      �� 5C      $��0,5,0,8q�6#   $��2��64   $��Seçili Senaryoyu��6A   .��5D      �� $��0,3,0,3q�6)   $��30��6:    �� Orientation$ 
Horizontal=�6F   .+�5E   "   �� $��50��6)   $��0,0,5,0q�64   $��2��6E   $��Right=�6U   $��	Güncelle��6q   ��6�   ��  �� ElementName$ wm�� �� Path$0 *DataContext.CommandTestCaseSelectedChanged���5F   &   `� 5G   *    `� 	Converter# [ 67   .1�5H   .   �� $ testCaseListView��66   $ SelectedItem�6U   �� 5I   .   $ testCaseListView��66   6W   5J   +   5L   #   �� 5M   "   $��0,0,5,0q�6)   $��3��6:   $��Right=�6J   $��50��6f   $��Kopyala��6q   ��6�   �� $ wm��$1 +DataContext.CommandCopySelectedTestCaseText���6�   �� $ testCaseListView��$ SelectedItem��� 5N   "   $��0,0,5,0q�6)   $��4��6:   $��Right=�6J   $��50��6f   $	��Sil��6q   ��6   �� $ wm��$/ )DataContext.CommandDeleteSelectedTestCase���6�   �� $ testCaseListView��$ SelectedItem�5O      5P      5Q      �� 5R      -   5         ��Name$ testCaseListView��5R   �   $��2��6   $��Disabled=�6,   ��. 
������� 6�   �� *SelectedTestCase+$ TwoWay=�$ PropertyChanged=���6  �� *SelectedProject.TestCases+ 5S        5T      $ SelectionChanged��6.   . 5U   "      69   �� *SelectedItemChangedCommand+   CommandParameter 6i   �� $ listv��5V      ��5Y      �� 5Z        �� 
TargetType"  ��6$   ! �� Triggers! 5[   "   ^� 5\   &   " ^� 
Conditions" 5]   *   �� 5^   .   # �� Binding# 68   �� $ wm��$ DataContext.IsModeUpdate�$ �� Value$ True6z   0�5`   *   �� 5a   .   % �� Property% � {�& �� Value& . 6J   5c   '   ^� 5d   &   " 5e   *   �� 5f   .   # 68   �� $ wm��$ DataContext.IsModeUpdate�$ True6z   0�5h   *   �� 5i   .   % � {�& .  6J   5k   '   5m      ��5o      �� 5p      .R�5q   "   �� .��5r   &   �� 67   5s   #   5t      ��5w      �� 5x      "  ��6$   .�5y   "   �� % � ��$& Stretch=�6O   5z      ��5|      �� 5}      .R�5~   "   �� ' ��Name$' brdItem��6)   $ 3q�6:   ��5   &   �� 5�   *   "  ��60   ! 5�   .   �� 5�   2   ( �� Binding( 6>   �� ) �� RelativeSource) �� * �� AncestorType$* ListViewItemL�+ �� Mode$+ FindAncestor=�$ 
IsSelected�, �� Value, True6�   .a�5�   6   �� % � ��
& �����6S   5�   3   �� 5�   2   ( 6>   �� ) �� $* ListViewItemL�$+ FindAncestor=�$ 
IsSelected�	, False6�   .a�5�   6   �� % � ��
& �����6S   5�   3   5�   +   ._�5�   &   �  5�   *   �� 5�   .   $��35 �6?   �� 5�   .   $��* �6?   �� 5�   .   $
��Auto �6?   �� 5�   .   $
��Auto �6?   �� 5�   .   $
��Auto �6?   .+�5�   *   �� $��0��61   $��35��6A   
�������
�������$��1q�6w   ._�5�   .   �� $
��Left=�65   
���   �$��1q�6_   ._�5�   2   �� - � Cursor$
- Hand��69   ��6G   �� $ wm��$' !DataContext.CommandChangeTestCase���6�   �� .��5�   6   �� . ��Name$	. img��6<   $��30��6I   $��30��6T   $��Uniform=�6`   $
- Hand��6s   ��5�   :   �� 5�   >   "  ��6D   ! 5�   B   �� 5�   F   $ AltoTestManager.TestCaseStatus/   Success#, Z/ 6q   ( 6R   �� *
CaseStatus+.a�5�   J   �� % � ��$& img/tickmark.png��6c   5�   G   �� 5�   F   0   Failed#, Z0 6q   ( 6R   �� *
CaseStatus+.a�5�   J   �� % � ��$& img/close.png��6c   �� 5�   J   % � ��$& Uniform=�6d   5�   G   5�   ?   5�   7   5�   3   5�   /   5�   +   �� 5�   *   $��Stretch=�64   $��1��6R   $
��Wrap=�6b   1 �� Padding$1 5,0,0,0q�6v   ��6�   �� *Description+5�   +   5�   '   5�   #   5�      5�      �� 5�      $��Bottom=�6�   $��3��6�   $#��Test Senaryosu Ekle/Güncelle��6�   {�6   �� *SelectedProject+2 �� 	Converter#2 [  .��5�      �� .+�5�      ]� $��2,5,5,5q�6(   ��. .+�5�   "   �� $��Description:��6(   $
��Bold"�6?   6S   �� 5�   "   -   5        3 ��Name$3 txtCaseDesc��5�   �   4 �� AcceptsReturn4 .  $	��100��6@   5 �� TextWrapping$
5 Wrap=�6P   $��Stretch=�6�   ��6d   �� *&$SelectedTestCaseToUpdate.Description+$ TwoWay=�$ PropertyChanged=�.��6    5�   &   �� 5�   *   $
 Enter��65    6A   �� *CommandAddNewTestCase+ 6k   �� $ txtCaseDesc��$
 Text�5�   +   �� 5�   *   $	 Shiftf�65   $
 Enter��6G    6S   �� *CommandAddNewLine+ 6y   �� $ txtCaseDesc��5�   +   .��5�   =   5�   #   5�      ]� 5�      .+�5�   "   �� -   5        6 ��Name$6 
btnModeNew��5�   �   $��Right=�6�   $��30��6�   $��0,0,5,5q�6�   $��Right=�6�   $
��Ekle��6  ��6)   �� $ wm��$' !DataContext.CommandAddNewTestCase���6t   �� $ txtCaseDesc��$
 Text���5�   &   �� 5�   *   "  ��60   ! 5�   .   �� 5�   2   ( 6>   �� *IsModeUpdate+	, False6_   .a�5�   6   �� % � {�& . 6a   5�   3   �� 5�   2   ( 6>   �� *IsModeUpdate+, True6_   .a�5�   6   �� % � {�& .  6b   5�   3   5�   +   5�   #   �� 5�   "   -   5        $6 btnAddNewCase��5�   �   $��Right=�6�   $��30��6�   $��0,0,5,5q�6   $��Right=�6  $��Güncellemeyi Bitir��6.  ��6)   �� $ wm��$) #DataContext.CommandChangeUpdateMode���6v   �� $ testCaseListView��$ SelectedItem���5�   &   �� 5�   *   "  ��60   ! 5�   .   �� 5�   2   ( 6>   �� *IsModeUpdate+, True6_   .a�5�   6   �� % � {�& . 6a   5�   3   �� 5�   2   ( 6>   �� *IsModeUpdate+	, False6_   .a�5�   6   �� % � {�& .  6b   5�   3   5�   +   5�   #   5�      5�      5�      �� 5�      $��4q�6    $��4��6�   $��30��6�   
���������64   �� *Notification.Type+#2 [ 7 �� NotifyOnTargetUpdated7 . .@�5�      �� $
��Left=�6$   $��Center=�6?   $
��Bold"�6Z   ��6l   �� *Notification.Text+7 . 8 � Triggers8 5�      :� 5�   "   9 :� RoutedEvent$9 Binding.TargetUpdated��6/   .[�5�   &   �� .��5�   *   �� .��5�   .   U� : �� TargetProperty$: Opacity�6L   .\�5�   2   9  3System.Windows.Media.Animation.EasingDoubleKeyFrame  ; O� KeyTime$; 0��6G   < O� Value$< 0Q�6S     5�   2   $; 0:0:0.25��6G   $< 1Q�6Z     5�   2   $; 0:0:4��6G   $< 1Q�6W     5�   2   $; 0:0:5��6G   $< 0Q�6W   5�   /   5�   +   5�   '   5�   #   ��5�      �� 5�   "   "  ��6(   ! 5�   &   �� 5�   *   ( 66   �� *Notification.Type+, 16\   .a�5�   .   �� % � ��
& � � �6[   5�   +   �� 5�   *   ( 66   �� *Notification.Type+, 06\   .a�5�   .   �� % � ��
& �   �6[   5�   +   �� 5�   *   ( 66   �� *Notification.Type+, -16\   .a�5�   .   �� % � ��
& �  ��6Y   5�   +   5�   #   5�      8 5      :� 5     $9 Binding.TargetUpdated��6,   .[�5  "   �� .��5  &   �� .��5  *   t� $: BorderThickness�5  	   = g� Duration$= 0:0:7D�5  	   > g� FillBehavior$> HoldEnd=�6   ? t� From$? 1,1,1,1X�61   	@ t� To$@ 0,0,0,0X�6@   5  '   5  #   5	     5     5     � 5     $��10,0,0,0q�6   $��1��6)    5     �� 5     �� 5     $��30 �6(   �� 5     .+�5     �� $��0,5,10,5q�6   
������ 
���   �$��1q�6\   ._�5     �� -   5        $. 
imgElement��5      $
��Fill=�64   ��5     �� 5  "   $ SelectedImagePath�6*   #2 [ 6D   A �� TargetNullValueA 6~   Z� 5  #   5     5     �� 5     $��1��6   $��Görseli Panodan Al��6w   ��6*   �� *CommandGetImageFromClipboard+��6[   Z� 6w   �� 5     $��2��6    $��
Görseller��6-   .��5     �  5     �� 5  "   $��* �63   �� 5   "   $��* �63   .+�5#     �� -   5        $ listv��5#  (   $��0,5,4,0q�68   ��. 
������� 6o   �� *SelectedImagePath+$ TwoWay=�$ PropertyChanged=���6�   �� *SelectedTestCase.ImagePaths+��5%  "   �� 5&  &   .R�5'  *   �� .��5(  .   �� 6?   5)  +   5*  '   ��5,  "   �� 5-  &   .R�5.  *   � 
������  5/  .   d� 50  2   B d� MouseAction$B LeftDoubleClicke�6?    51  ;   �� $ wm��$- 'DataContext.CommandShowLargeImageWindow� 52  ;   �� 6Z   .+�54  .   �� $ 3q�65   $��Stretch=�6A   
����ح�$��1q�6w   ._�55  2   ]� ��. .+�56  6   �� $��0,0,5,0q�6=   $��Center=�6N   $	��Sil��6i   $��Right=�6w   $��20��6�   $��20��6�   ��6�   �� $ wm��$0 *DataContext.CommandDeleteSelectedImagePath���6�   �� �� 57  6   $��0,0,5,0q�6=   $��Center=�6N   $��Kopyala��6i   $��Right=�6{   $��50��6�   $��20��6�   ��6�   �� $ wm��$- 'DataContext.CommandCopyImageToClipboard���6�   �� �� 59  6   $��Center=�6=   ._�5:  :   �� $��Center=�6F   $��5,0,0,0q�6a   $
��Left=�6r   $��20��6�   ��6�   �� 6�   5;  7   5<  3   5=  /   5>  +   5?  '   5A     �� 5B     
����ح�$��1��6=   4 .  $
5 Wrap=�6c   $��Stretch=�6�   ��6w   �� *#!SelectedTestCaseToUpdate.TestData+$ TwoWay=�$ PropertyChanged=�.��6�   5C     5D     5E     5F     5G     5H     �� 5I  
   $��Test Projesi Ekle/Sil��6   .��5J     �� .+�5K     �� $ 
Horizontal=�6   .+�5L     	� $
��Test��6M   $��Center=�6\   $��20��6~   $	��100��6�    6"   �� *AddIsTestEnvironment+6�   	� 5M     $��Preprod��6P   $��Center=�6b   $��20��6�   $	��100��6�    6"   �� *AddIsPreprodEnvironment+6�   5N     �� 5O     $��Test Projesi Ekle��6   .��5P     � $��5q�6    5Q     �� 5R     �� 5S      5U     �� 5V     $
��Auto �6/   �� 5W     $��* �6/   �� 5X     $
��Auto �6/   .+�5[     �� $��1��6    $	��Top=�6-   $��30��6E   $��Proje Adı:��6Q   �� 5\     -   5        $3 txtProjName��5\  /   $��1��6"   $��1��6D   $	��Top=�6T   $��Center=�6m   $��30��6�   6�   !  AltoTestManager.ImageButton  5]     $��1��6,   $��2,0,0,0q�5^  k   $��2��6|   $	��Top=�6�   $��30��6�   $��30��6�   C   Source$C img/add.png��6�   D   CommandD 5]  9   �� *CommandAddNewTestProject+E   CommandParameterE 5^  +   �� $ txtProjName��$
 Text�6�   5_     5`     �� 5a     $��0,10,0,0q�6   $��Test Projesi Sil��6-   .��5b     � $��5q�6    5c     �� 5d     $
��Auto �6/   �� 5e     $��* �6/   �� 5f     $
��Auto �6/   .+�5h     �� $	��Top=�6    $��30��68   $��Proje Seç:��6D   �� 5i     -	   5        F ��Name$F 
cmbProjSil��5i  #   $��1��67   $	��Top=�6G   $��Center=�6`   $��30��6�   $ Caption��6�   ��6�   �� *EditTestProjects+6�     5j     $��2,0,0,0q�6�   $��2��6�   $	��Top=�6�   $��30��6�   $��30��6�   $C img/close.png��6�   D 6,   �� *CommandDeleteTestProject+E 6Y   �� $ 
cmbProjSil��$ SelectedItem�6  5k     5l     �� 5n     $��0,10,0,0q�6   $��Test Projesi Güncelle��6-   .��5o     � $��5q�6    5p     �� 5q     $
��Auto �6/   �� 5r     $��* �6/   �� 5s     $
��Auto �6/    5u     �� 5v     �� 5w     .+�5y     �� $	��Top=�6    $��30��68   $��Proje Seç:��6D   �� 5z     -
   5        $F cbmUpdateProj��5z  #   $��1��6:   $	��Top=�6J   $��Center=�6c   $��30��6�   $ Caption��6�   ��6�   �� *EditTestProjects+6�   �� 5{     $��0,10,0,0q�6    $��1��62   $	��Top=�6?   $��30��6W   $��Proje Adı:��6c   �� 5|     $��0,10,0,0q�6"   $��1��64   $��1��6A   $	��Top=�6Q   $��Center=�6j   $��30��6�   ��6�   �� $ cbmUpdateProj��$ SelectedItem.Caption�$ TwoWay=�$ PropertyChanged=�6  5}     5~     5     5�     �� 5�  
   $��Analiz ve Notlar��6   5�     �� 5�  
   $��Ayarlar��6   .��5�     �  5�     �� 5�     $	��27* �6'   �� 5�     $
��316* �6'    5�     �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     .+�5�     �� $��3,3,3,0q�6   $	��Top=�6.   $ 
Horizontal=�6F   $��2��6_   .+�5�     �� $
��Bold"�6    
���   �$��3,0,3,0q�6E   $
��Left=�6V   $��Center=�6q   $��Saklama Yeri:��6�   �� 5�     
�������G �� 
IsReadOnlyG . $��Center=�6H   $
��Left=�6j   $��30��6�   $	��400��6�   ��6�   �� *
DataFolder+$ TwoWay=�$ PropertyChanged=�6�   �� 5�     $��3q�6   $��Klasör Seç��6)   $��30��6c   $��4,0,0,0q�6o   $��Center=�6�   ��6>   �� *SelectDataFolder+6�   5�     �� 5�     $��3,10,0,0q�6   $
��Left=�6-   $	��Top=�6H   $��1��6`   $%��Word dosyasını farklı kaydet��6�   $��2��6�    6m   �� *WordOpenSaveAsDialog+$ TwoWay=�$ PropertyChanged=�6�   �� 5�     $	��Top=�6   $��30��66   $��2��6B   $��2��6V   $ 
Horizontal=�6c   .+�5�     �� $��20��6   $��1,10,0,0q�6)   $��Update Program��6<   $
��Left=�6U   $	��120��6p   ��6}   �� *UpdateProgramCommand+�� 5�     $��3,0,0,0q�6   $	��250��6~   ��6/   �� *UpdaterPath+$ TwoWay=�$ PropertyChanged=�6�   5�     �� 5�     $��1,10,0,0q�6   $��3��61   $ 
Horizontal=�6>   $
��Left=�6W   $	��Top=�6r   $��2��6�   .+�5�     �� $��Görsel boyutu��6   �� 5�     $	��200��6   ��6+   �� *StretchEnumList+ 6S   �� *SelectedStretch+$ TwoWay=�$ PropertyChanged=�5�     5�     5�     5�     5�     