   M S B A M L   `   `   `  ����	  Pairs    Pairs.Pairs_game   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          �Name$  Pairs_game1��5   	    � Title$ Pairs game - Play��5   	   $	��480��6#   $	��640��60   $	��640��6<   $	��480��6K   $��14(�6[    � Icon$ 	Pairs.ico��6i   .��5      � .+�5   
   o� -   5         o�Name$ 
Pairs_Menu��5      $��26��6#   $	��Top=�6/   $��14(�6G   
���   �$��0,0,0,1q�6i   .@�5      m� $
��File��6   .@�5      m� -   5        $��New Game��5      6-   m� 5	      -   5        $��	Open Game��5	      6.   m� 5
      -   5        $��	Save Game��5
      6.   m� 5      -   5        $
��Exit��5      6)   5      m� 5      -   5         m�Name$ Options��5      $��Options��6(   .@�5      m� -   5        $ Beginner��5      $��Beginner��6-    m� IsCheckable .  m� 	IsChecked . 6c   m� 5      -	   5        $ Intermediate��5      $��Intermediate��61    . 6Z   m� 5      -
   5        $ Custom��5      $��Custom��6+    . 6N   5      m� 5      $
��Help��6   .@�5      m� -   5        $��About��5      6*   m� 5      -   5        $
��Info��5      6)   5      5      �� 5   
   -   5         ��Name$ 	StatusBar��5      $��26��6'   $��Bottom=�63   $	��0,1q�6N   $��0q�6d   
���   ���5      �� 5      .R�5      �  � ColumnDefinitions 5      �� 5      $
��Auto �6/   $��80��6<   �� 5      $
��Auto �6/   $��70��6<   �� 5      $
��Auto �6/   $��25��6<   �� 5      $
��Auto �6/   $��5��6<   �� 5       $
��Auto �6/   $��50��6<   �� 5!      $
��Auto �6/   $��61��6<   �� 5"      $
��Auto �6/   $��10��6<   �� 5#      $
��Auto �6/   $��10��6<   �� 5$      $
��Auto �6/   $��10��6<   �� 5%      $
��Auto �6/   $��5��6<   �� 5&      �� 5'      $
��Auto �6/   $��36��6<   �� 5(      $
��Auto �6/   $��10��6<   �� 5)      $
��Auto �6/   $��5��6<   �� 5*      $
��Auto �6/   $��82��6<   �� 5+      $
��Auto �6/   $��60��6<   5-      5.      .@�50      �� $��0��6   .��51      �� -   5        $��lblUserNametext��51      $��user name: ��63   $��14(�6F   52      �� 53      $��1��6   .��54      �� -   5        $��lblUserName��54      $��*��6/   $��14(�68   55      �� 56      $��2��6   .��57      �� -   5        $��lblUserNameImg��57      58      �� 59      $��3��6   �� 5:      $��4��6   .��5;      �� -   5        $��lblOptionsText1��5;      $��Format��63   $��14(�6A   5<      �� 5=      $��5��6   .��5>      �� -   5        $��
lblOptions��5>      $��Beginner��6.   $��14(�6>   5?      �� 5@      $��6��6   .��5A      �� -   5        $��lblOptionsM��5A      $��4��6/   $��14(�68   5B      �� 5C      $��7��6   .��5D      �� -   5        $��lblOptionsX��5D      $��x��6/   $��14(�68   5E      �� 5F      $��8��6   .��5G      �� -   5        $��lblOptionsN��5G      $��4��6/   $��14(�68   5H      �� 5I      $��9��6   �� 5J      $��10��6   �� 5K      $��11��6   .��5L      �� -   5        $��lblOptionsScorText��5L      $��Scor:��66   $��14(�6C   5M      �� 5N      $��12��6   .��5O      �� -   5        $��lblOptionsScor��5O      $��0��62   $��14(�6;   5P      �� 5Q      $��13��6   �� 5R      $��14��6   .��5S      �� -   5        $��lblCowntDownTimerText��5S      $��Timp ramas:��69   $��14(�6L   5T      �� 5U      $��15��6   .��5V      �� -   5        $��lblCowntDownTimer��5V      $��00:00:00��65   $��Right=�6E   $��14(�6a   5W      5X      �� 5Y   
   $
��Auto=�6   $��Stretch=�6<   $��Stretch=�6a   ��. $
��0,26q�6�   $
��Auto=�6�   .��5Z      � -   5        	 �Name$	 gridTabelaJoc��5Z      ���	#FF5D5D5D$��1��6A   $��1��6N   $
��Auto=�6^   $
��Auto=�6�   5[      5\      5]      