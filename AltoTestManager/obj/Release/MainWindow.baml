   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  J FAltoTestManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:AltoTestManager.ConvertersAltoTestManager.Converters 1clr-namespace:AltoTestManagerAltoTestManager c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity     AltoTestManager.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6sys&clr-namespace:System;assembly=mscorlib     9conv(clr-namespace:AltoTestManager.Converters     /localclr-namespace:AltoTestManager     _iePclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     -     �Name$  wm�� � Title$ Test Manager��$	��600��$	��700�� � Icon$ img/favicon.ico��.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ Closing��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *CommandSaveJson+ � 	Resources 7 1AltoTestManager.SelectedProjectToEnabledConverter %  !SelectedProjectToEnabledConverter( "AltoTestManager.ImageNullConverter  ImageNullConverter2  ,AltoTestManager.StringToImageSourceConverter   StringToImageSourceConverterG AAltoTestManager.Converters.NotificationTypeToBorderColorConverter * &NotificationTypeToBorderColorConverterE	 ?AltoTestManager.Converters.IsModeUpdateToButtonContentConverter ( $IsModeUpdateToButtonContentConverter5
 /AltoTestManager.Converters.ObjectArrayConverter  ObjectArrayConverter��%Z   &	        &	      &	 
     &	      &	      &	              	  
   J� InputBindings �� 	 �� 	Modifiers$	 Controlf�

 �� Key$
 V�� �� Command �� *CommandGetImageFromClipboard+ �� CommandParameter Z� .���� .@��� $��Test Senaryoları��.���  � ColumnDefinitions �� �� .+��  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto �.+��  �� �� �� .+�]� $��0,4,0,0q�.+�	� $
��Test��$��Center=�$��20��$��80�� d� 	IsChecked �� *IsTestEnvironment+ �� Mode$ TwoWay=�	� $��Preprod��$��Center=�$��20��$��80�� �� *IsPreprodEnvironment+$ TwoWay=��� $	��140��$��Right=�$��Right=�$��25������ *CommandCloneTestProject+���� *SelectedProject+���� *CloneButtonContent+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�]� $��1����. .+��� $��Right=�$��25��$��Worde Çıkart������ *CommandExportTestProjectWord+���� *SelectedProject+�� $��1�� �� Padding$ 2q�$��5q�$��Stretch=�$��Center=�
���   �$��1q�._���  �� StaysOpenOnEdit .   �� IsTextSearchCaseSensitive .   �� IsTextSearchEnabled .  �� 
IsEditable .  w� TextPath$ Caption�� �� DisplayMemberPath$ Caption�� �� SelectedItem �� *SelectedProject+$ TwoWay=�$ PropertyChanged=����� *TestProjectsByEnv+�� $��0,5,0,8q�$��2��$��Seçili Senaryoyu��.���� $��0,3,0,3q�$��30�� �� Orientation$ 
Horizontal=�.+��� $��50��$��0,0,5,0q�$��2��$��Right=�$��	Güncelle������  �� ElementName$ wm�� �� Path$0 *DataContext.CommandTestCaseSelectedChanged���`�  `� 	Converter# [ .1��� $ testCaseListView��$ SelectedItem��� $ testCaseListView���� $��0,0,5,0q�$��3��$��Right=�$��50��$��Kopyala������ $ wm��$1 +DataContext.CommandCopySelectedTestCaseText����� $ testCaseListView��$ SelectedItem��� $��0,0,5,0q�$��4��$��Right=�$��50��$	��Sil������ $ wm��$/ )DataContext.CommandDeleteSelectedTestCase����� $ testCaseListView��$ SelectedItem��� -    ��Name$ testCaseListView��$��2��$��Disabled=���. 
������� �� *SelectedTestCase+$ TwoWay=�$ PropertyChanged=����� *SelectedProject.TestCases+   $ SelectionChanged��.    �� *SelectedItemChangedCommand+   CommandParameter �� $ listv������   �� 
TargetType"  ��! �� Triggers! ^� " ^� 
Conditions" �� # �� Binding# �� $ wm��$ DataContext.IsModeUpdate�$ �� Value$ True0��� % �� Property% � {�& �� Value& . ^� " �� # �� $ wm��$ DataContext.IsModeUpdate�$ True0��� % � {�& .  ���� .R��� .���� ���� "  ��.��� % � ��$& Stretch=����� .R��� ' ��Name$' brdItem��$ 3q����� "  ��! �� ( �� Binding( �� ) �� RelativeSource) �� * �� AncestorType$* ListViewItemL�+ �� Mode$+ FindAncestor=�$ 
IsSelected�, �� Value, True.a��� % � ��
& ������� ( �� ) �� $* ListViewItemL�$+ FindAncestor=�$ 
IsSelected�	, False.a��� % � ��
& �����._��  �� $��35 ��� $��* ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��35��
�������
�������$��1q�._��� $
��Left=�
���   �$��1q�._��� - � Cursor$
- Hand������ $ wm��$' !DataContext.CommandChangeTestCase����� .���� . ��Name$	. img��$��30��$��30��$��Uniform=�$
- Hand������ "  ��! �� $ AltoTestManager.TestCaseStatus/   Success#, Z/ ( �� *
CaseStatus+.a��� % � ��$& img/tickmark.png���� 0   Failed#, Z0 ( �� *
CaseStatus+.a��� % � ��$& img/close.png���� % � ��$& Uniform=��� $��Stretch=�$��1��$
��Wrap=�1 �� Padding$1 5,0,0,0q����� *Description+�� $��Bottom=�$��3��$#��Test Senaryosu Ekle/Güncelle��{��� *SelectedProject+2 �� 	Converter#2 [  .���� .+�]� $��2,5,5,5q���. .+��� $��Description:��$
��Bold"��� -   3 ��Name$3 txtCaseDesc��4 �� AcceptsReturn4 .  $	��100��5 �� TextWrapping$
5 Wrap=�$��Stretch=����� *&$SelectedTestCaseToUpdate.Description+$ TwoWay=�$ PropertyChanged=�.�� �� $
 Enter�� �� *CommandAddNewTestCase+ �� $ txtCaseDesc��$
 Text��� $	 Shiftf�$
 Enter�� �� *CommandAddNewLine+ �� $ txtCaseDesc��.��]� .+��� -   6 ��Name$6 
btnModeNew��$��Right=�$��30��$��0,0,5,5q�$��Right=�$
��Ekle������ $ wm��$' !DataContext.CommandAddNewTestCase����� $ txtCaseDesc��$
 Text����� "  ��! �� ( �� *IsModeUpdate+	, False.a��� % � {�& . �� ( �� *IsModeUpdate+, True.a��� % � {�& .  �� -   $6 btnAddNewCase��$��Right=�$��30��$��0,0,5,5q�$��Right=�$��Güncellemeyi Bitir������ $ wm��$) #DataContext.CommandChangeUpdateMode����� $ testCaseListView��$ SelectedItem����� "  ��! �� ( �� *IsModeUpdate+, True.a��� % � {�& . �� ( �� *IsModeUpdate+	, False.a��� % � {�& .  �� $��4q�$��4��$��30��
����������� *Notification.Type+#2 [ 7 �� NotifyOnTargetUpdated7 . .@��� $
��Left=�$��Center=�$
��Bold"����� *Notification.Text+7 . 8 � Triggers8 :� 9 :� RoutedEvent$9 Binding.TargetUpdated��.[��� .���� .��U� : �� TargetProperty$: Opacity�.\�9  3System.Windows.Media.Animation.EasingDoubleKeyFrame  ; O� KeyTime$; 0��< O� Value$< 0Q�  $; 0:0:0.25��$< 1Q�  $; 0:0:4��$< 1Q�  $; 0:0:5��$< 0Q����� "  ��! �� ( �� *Notification.Type+, 1.a��� % � ��
& � � ��� ( �� *Notification.Type+, 0.a��� % � ��
& �   ��� ( �� *Notification.Type+, -1.a��� % � ��
& �  ��8 :� $9 Binding.TargetUpdated��.[��� .���� .��t� $: BorderThickness�= g� Duration$= 0:0:7D�> g� FillBehavior$> HoldEnd=�? t� From$? 1,1,1,1X�	@ t� To$@ 0,0,0,0X�� $��10,0,0,0q�$��1�� �� �� $��30 ��� .+��� $��0,5,10,5q�
������ 
���   �$��1q�._��� -   $. 
imgElement��$
��Fill=����� $ SelectedImagePath�#2 [ A �� TargetNullValueA Z� �� $��1��$��Görseli Panodan Al������ *CommandGetImageFromClipboard+��Z� �� $��2��$��
Görseller��.���  �� $��* ��� $��* �.+��� -   $ listv��$��0,5,4,0q���. 
������� �� *SelectedImagePath+$ TwoWay=�$ PropertyChanged=����� *SelectedTestCase.ImagePaths+���� .R��� .���� ���� .R�� 
������  d� B d� MouseAction$B LeftDoubleClicke� �� $ wm��$- 'DataContext.CommandShowLargeImageWindow� �� .+��� $ 3q�$��Stretch=�
����ح�$��1q�._�]� ��. .+��� $��0,0,5,0q�$��Center=�$	��Sil��$��Right=�$��20��$��20������ $ wm��$0 *DataContext.CommandDeleteSelectedImagePath����� �� $��0,0,5,0q�$��Center=�$��Kopyala��$��Right=�$��50��$��20������ $ wm��$- 'DataContext.CommandCopyImageToClipboard����� �� $��Center=�._��� $��Center=�$��5,0,0,0q�$
��Left=�$��20������ �� -   $��txtTestData��
����ح�$��1��4 . $
5 Wrap=�$��Stretch=����� *SelectedTestCase.TestData+$ TwoWay=�$ PropertyChanged=�.���� $��Test Projesi Ekle/Sil��.���� .+��� $ 
Horizontal=�.+�	� $
��Test��$��Center=�$��20��$	��100�� �� *AddIsTestEnvironment+	� $��Preprod��$��Center=�$��20��$	��100�� �� *AddIsPreprodEnvironment+�� $��Test Projesi Ekle��.��� $��5q� �� ��  �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��1��$	��Top=�$��30��$��Proje Adı:���� -	   $3 txtProjName��$��1��$��1��$	��Top=�$��Center=�$��30��!  AltoTestManager.ImageButton  $��1��$��2,0,0,0q�$��2��$	��Top=�$��30��$��30��C   Source$C img/add.png��D   CommandD �� *CommandAddNewTestProject+E   CommandParameterE �� $ txtProjName��$
 Text��� $��0,10,0,0q�$��Test Projesi Sil��.��� $��5q� �� $
��Auto ��� $��* ��� $
��Auto �.+��� $	��Top=�$��30��$��Proje Seç:���� -
   F ��Name$F 
cmbProjSil��$��1��$	��Top=�$��Center=�$��30��$ Caption������ *EditTestProjects+  $��2,0,0,0q�$��2��$	��Top=�$��30��$��30��$C img/close.png��D �� *CommandDeleteTestProject+E �� $ 
cmbProjSil��$ SelectedItem��� $��0,10,0,0q�$��Test Projesi Güncelle��.��� $��5q� �� $
��Auto ��� $��* ��� $
��Auto � �� �� .+��� $	��Top=�$��30��$��Proje Seç:���� -   $F cbmUpdateProj��$��1��$	��Top=�$��Center=�$��30��$ Caption������ *EditTestProjects+�� $��0,10,0,0q�$��1��$	��Top=�$��30��$��Proje Adı:���� $��0,10,0,0q�$��1��$��1��$	��Top=�$��Center=�$��30������ $ cbmUpdateProj��$ SelectedItem.Caption�$ TwoWay=�$ PropertyChanged=��� $��Analiz ve Notlar���� $��Ayarlar��.���  �� $	��27* ��� $
��316* � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� .+��� $��3,3,3,0q�$	��Top=�$ 
Horizontal=�$��2��.+��� $
��Bold"�
���   �$��3,0,3,0q�$
��Left=�$��Center=�$��Saklama Yeri:���� 
�������G �� 
IsReadOnlyG . $��Center=�$
��Left=�$��30��$	��400������ *
DataFolder+$ TwoWay=�$ PropertyChanged=��� $��3q�$��Klasör Seç��$��30��$��4,0,0,0q�$��Center=����� *SelectDataFolder+�� $��3,10,0,0q�$
��Left=�$	��Top=�$��1��$%��Word dosyasını farklı kaydet��$��2�� �� *WordOpenSaveAsDialog+$ TwoWay=�$ PropertyChanged=��� $	��Top=�$��30��$��2��$��2��$ 
Horizontal=�.+��� $��20��$��1,10,0,0q�$��Update Program��$
��Left=�$	��120������ *UpdateProgramCommand+�� $��3,0,0,0q�$	��250������ *UpdaterPath+$ TwoWay=�$ PropertyChanged=��� $��1,10,0,0q�$��3��$ 
Horizontal=�$
��Left=�$	��Top=�$��2��.+��� $��Görsel boyutu���� $	��200������ *StretchEnumList+ �� *SelectedStretch+$ TwoWay=�$ PropertyChanged=�