   M S B A M L   `   `   `  ����H  DSocket Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Socket_ClientSocket_Client  5   	       Socket_Client.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Socket_Client     5   	   -   5          � Title$  Socket Client��5   	   $	��400��6   $	��600��6,    � WindowStartupLocation$ CenterScreen=�5
   	   .��5      �� .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��Center=�6.   $��	0 10 0 10q�6K   .+�5      �� $��20(�6   
Count down62   �� 5      -   5        $��	CountDown��5   8   $��20(�6   $
��Bold"�6&   $��5 0 0 0q�6I   5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��Products_DataGrid��5      $	��240��6,   $��10q�69      GridLinesVisibility$
 None=�6E      AutoGenerateColumns .     
IsReadOnly . $��14(�6�    J� IsHitTestVisible .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Index��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 1* 64   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6?   �� *index+  5      $
 Name��6%   $	 4* 63   
 6>   �� *name+  5      $ Price��6%   $	 2* 64   
 6?   �� *price+5      �� 5   
   $ 
Horizontal=�6   $��
10 10 10 0q�6.   $��Center=�6B   .+�5      �� $��0 5 10 0q�6   $��14(�6*   Choose a product6J   �� 5      -   5        $��Products_ComboBox��5      $	��150��60   $��0 0 10 0q�6<   $��14(�6N   ��5      �� 5      .R�5      �� $��25��6:    �� 
LineHeight$ 22��6F    �� LineStackingStrategy$ BlockLineHeight=�6V   ��6$   �� *name+6V   5      5      �� 5       $��0 5 10 0q�6   $��14(�6*   Price6?   �� 5!      -   5        $��Price_TextBox��5!      $	��150��6+   $��0 0 10 0q�67   $ 24��6I   $ BlockLineHeight=�6c   $��14(�6�   �� 5"      -   5        $
��Send��5"      $��70��68   $��30��6C    �� 	IsDefault . 6O   5#      5$      5%      