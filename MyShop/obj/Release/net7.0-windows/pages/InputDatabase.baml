   M S B A M L   `   `   `  ���� A  =MyShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:MyShopMyShop       MyShop.pages.InputDatabase   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     &localclr-namespace:MyShop     -     � WindowStartupLocation$  CenterScreen=� � WindowStyle$
 None=� � AllowsTransparency$
 True��
������  � Title$ InputDatabase��$	��500��$	��800�� � 	Resources (   "MyShop.RelativeToAbsoluteConverter   absoluteConverter��%   &	          .���  � ColumnDefinitions �� $��* ��� $
��1.5* �.+��� -   $��closeImageButton��$��1�� K� ZIndex$ 1��  imgClose#��[ ���� *
closeImage+ �� 	Converter# [  ��  �� CornerRadius$ 	10 0 0 10������ 	 �� 
StartPoint$		 0,0&�
 �� EndPoint$	
 1,1&�.���  � Color$ Bisque�� � Offset$ 0Q�� $ #3AADA1��$ 1Q�._�� .+��� .+��  � Points �    -1��� ;X�����#4EB1B6� ' �    �� �� ���� ��
�����#4EB1B6A� $��250 450 0 0q�$��40��$��40�����#4EB1B6A� $��
50 400 0 0q�$��20��$��20�����#4EB1B6�� $��Center=�.+��� $Q��KEnter your database's name                                to create/connect��$
��Wrap=�$��Italic$� J� Opacity$	 0.8Q�$��20 30 20 30q�  
NormalText#��[ �� $	 100��$	��100��$	��100�����#FFFFFF$��15 40 20 20q�._�A� $��95��$��95������  j� Stretch$ Uniform=� �� ImageSource �� *	dbImage+# [  �� -   $��borderDatabase�����#FFFFFF$��1��$ 	0 10 10 0��._��� $��Center=�.+��� $��Enter your database�����#3AB19B  	TitleText#��[ �� ���#3AB19B$��1q�$
��70 7q�$ 5��._�� $	��7 9q� �� $
��auto ��� $��* �.+��� -    ��Name$ textDatabase��$��Database's Name��  textHint#��[ �� -    ��Name$ txtDatabase��  textBox#��[ �� -    ��Name$ connectDatabaseButton��$��Connect��  
mainButton#��[ �� -   $ 
backButton��$
��Back��#��[ �� $�� �����#3AB19B#��[ � -    �Name$ 
loadingBar��$��20��$	��150��$y�Hidden=�