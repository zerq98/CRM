   M S B A M L   `   `   `  ����K  GCRM.Desktop.View, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:CRM.Desktop.View.ViewsCRM.Desktop.View.Views  5   	   '    !CRM.Desktop.View.Views.MainWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:CRM.Desktop.View.Views     5   	     � Title$	  CRM��5   	   $	��450��6   $	��800��6%    � WindowStartupLocation$ CenterScreen=�6g   ��64   �� *SideBarWidth+ �� Mode$ OneWayToSource=�.��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��200 �6   �� 5      $��* �6   .+�5   
   � $��0��6   ���#242424.+�5      ��  �� Orientation$ Vertical=�6   $��Stretch=�60   $��Stretch=�6N   .+�5      �� $	��CRM��6   
������� �� TextAlignment$ Center=�6:   $��55(�6Q   $
��Bold"�6_   $��0,0,0,25q�6q   6�   �� 5      $��
Statystyki��6   
������    Button.Hovergray#���   6G   
�������$
��Bold"�6�   $��28(�6�   $��Stats��6�   ��6�   �� *ChangeViewCommand+6�   �� 5      $��Użytkownicy��6   
������ #���   6H   
�������$
��Bold"�6�   $��28(�6�   $��Users��6�   ��6�   �� *ChangeViewCommand+6�   �� 5      $��
Dane firmy��6   
������ #���   6G   
�������$
��Bold"�6�   $��28(�6�   $��Company��6�   ��6�   �� *ChangeViewCommand+6�   �� 5      $��Wyjdź��6   
������ #���   6B   
�������$
��Bold"�6�   $��28(�6�   ��6�   �� *ExitCommand+6�   5      5      � 5   
   $��1��6   ���#326cfc 5      �� 5      $��20 �6#   �� 5      $��* �6#   �� 5      $��20 �6#    � RowDefinitions 5      �� 5      $��20 �6    �� 5      $��* �6    �� 5       $��20 �6    .+�5"      �� $��1��6   $��1��6*   $��Stretch=�6:   $��Stretch=�6X   ��6t   �� *ActiveControl+ �� UpdateSourceTrigger$ PropertyChanged=�5#      5$      5%      