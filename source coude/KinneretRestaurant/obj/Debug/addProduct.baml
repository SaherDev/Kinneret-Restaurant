   M S B A M L   `   `   `  ����M  IKinneretRestaurant, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:KinneretRestaurantKinneretRestaurant  5   	   #    KinneretRestaurant.addProduct   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:KinneretRestaurant     5   	     � Title$  
addProduct��5   	   $	��475��6   $	��416��6)   .��5	      � -   5         �Name$
 grid��5	      ���	#FFECF1F7.+�5
   
   �� -   5         ��Name$ 	btndelete��5
      $��
מחיקה��6<   $
��Left=�6L   $��225,387,0,0q�6g   $	��Top=�6|   $��88��6�   $��David)�6�   $��15.938(�6�   $��29��6�   $
��Bold"�6�    J� RenderTransformOrigin$ 
0.34,0.517&�6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGridView��5      ���	#FFF0F0F0   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6@   $	��282��6P   $	��Top=�6]   $��
10,10,10,0q�6u   $��David)�6�   $��14(�6�   $��Medium"�6�      ColumnHeaderHeight$ 24Q�6�      	RowHeight$ 24Q�6�   �� 5   
   -   5        $ btnadd��5      $��
הוספה��6!   $
��Left=�61   $��
95,387,0,0q�6L   $	��Top=�6`   $��90��6x   $��David)�6�   $��15.938(�6�   $��29��6�   $
��Bold"�6�   �� 5   
   -   5         ��Name$ textBoxProduct��5      $
��Left=�6*   $��30��6E   $��266,332,0,0q�6Q   	 �� TextWrapping$
	 Wrap=�6f   $�� ��6z   $	��Top=�6�   $	��102��6�   $ 0.716,0.429&�6�   $��15.938(�6�   
 �� TextAlignment$
 Right=�6�    �� TextDecorations 6�   Z� 6�   �� 5   
   -   5         ��Name$ label_Copy2��5      $��שם מוצר��6%   $
��Left=�67   $��305,308,0,0q�6R   $	��Top=�6g   $��David)�6   $
��Bold"�6�   $��	15.937767(�6�   $��24��6�   $ 1.012,0.436&�6�   ��5      �� 5      5      �� 5   
   -   5        $ textBoxCategory��5      $
��Left=�6+   $��30��6F   $��154,332,0,0q�6R   $
	 Wrap=�6g   $�� ��6{   $	��Top=�6�   $��95��6�   $ 0.716,0.429&�6�   $��15.938(�6�   $
 Right=�6�    6�   Z� 6�   �� 5   
   -   5        $ textBoxPrice��5      $
��Left=�6(   $��30��6C   $��
38,332,0,0q�6O   $
	 Wrap=�6c   $�� ��6w   $	��Top=�6   $��98��6�   $ 0.716,0.429&�6�   $��15.938(�6�   $
 Right=�6�    6�   Z� 6�   �� 5   
   -	   5        $ label_Copy1��5      $��מחיר��6%   $
��Left=�64   $��
95,308,0,0q�6O   $	��Top=�6c   $��David)�6{   $
��Bold"�6�   $��	15.937767(�6�   $��24��6�   $ 1.012,0.436&�6�   ��5      �� 5      5      �� 5   
   -
   5        $ 
label_Copy��5      $��קטגוריה��6$   $
��Left=�66   $��186,308,0,0q�6Q   $	��Top=�6f   $��David)�6~   $
��Bold"�6�   $��	15.937767(�6�   $��24��6�   $ 1.012,0.436&�6�   ��5      �� 5      5      5       5!      