   M S B A M L   `   `   `  ����  CPT-TCP-win    CPT_TCP_win.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$	  CPT��5   	   $	��350��6   $	��525��6"   $��0,0,0,1q�6.   $y�Visible=�6H    � Icon$ Untitled-1.ico��6]   ��5      �� 5   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ 	#FF2A30B0��6   � 5      $ White��6    � Offset$ 1Q�6)   5	      .��5      � ��5   
   �� 5      $ 0.5,1&�6"    � MappingMode$ RelativeToBoundingBox=�63   $ 0.5,0&�6W   .��5      � $ 	#FF666666��6   $ 0Q�61   � 5      $ White��6   $ 0.142Q�6-   5       � RowDefinitions 5   
   �� 5      $
��184* �6   �� 5      $
��135* �6    � ColumnDefinitions 5   
   �� 5      $��0* �6   �� 5      $
��236* �6   �� 5      $
��281* �6   .+�5   
   �� -   5        	 ��Name$	 
btnConnect��5      $��Connect��6%   $��2��67   $��Right=�6G   $��	0,69,10,0q�6c   $	��Top=�6v   $��89��6�   $��20��6�   
�������6�   �� 5   
   -   5        $	 
btnEncrypt��5      $��Encrypt��6%   $��2��67   $��Right=�6G   $��	0,94,10,0q�6c   $��89��6v   $��20��6�   $	��Top=�6�   
�������6�   �� 5   
   -   5        $	 btnSend��5      $
��Send��6"   $��2��61   $��0,0,3,10q�6A   $��1��6i   $��23��6v   $��Bottom=�6�   $��Right=�6�   $��96��6�   
�������6�   �� 5   
   $��Messages��6   $
��Left=�6#   $��1,10,0,0q�6>   $	��Top=�6P   $��26��6h   $��62��6t   $��1��6   �� 5   
   -   5        
 ��Name$
 txtSend��5      $��2��6#   $��10,0,104,10q�67    �� TextWrapping$
 Wrap=�6L   $��Write a message here��6`   $��1��6�   $��1��6�   $��23��6�   $��Bottom=�6�   
�������6�   �� 5    
   $��Reciever IP��6   $��2��6&   $��	0,10,31,0q�66   $��Right=�6I   $��68��6e   $��26��6p   $	��Top=�6|   �� 5!   
   -   5        $
 txtIP��5!      $��2��6!   $��Right=�61   $��	0,41,10,0q�6M   $
 Wrap=�6`   $��	127.0.0.1��6t   $��89��6�   $��23��6�   $	��Top=�6�   �� 5"   
   -   5        $	 btnAbout��5"      $��About��6#   $��2��63   $��Right=�6C   $��
0,119,10,0q�6_   $��89��6s   $��20��6�   $	��Top=�6�   
�������6�   �� 5#   
   -   5        $
 txtMessages��5#      $��3��6'   $��10,41,104,38q�6;   $
 Wrap=�6Q   $��2��6e   ��. " �� VerticalScrollBarVisibility$ Visible=�6�   
�������6�   �� 5$   
   -   5        $	 btnUPnP��5$      $
��UPnP��6"   $��2��61   $��Right=�6A   $��
0,144,10,0q�6]   $��89��6q   $��20��6�   $	��Top=�6�   
�������6�   �� 5%   
   -	   5        $
 
txtLocalIP��5%      $��2��6&   $
��Left=�66   $��23��6Q   $��
182,21,0,0q�6]   $
 Wrap=�6q   $	��Top=�6�   $��89��6�   $��1��6�   �� 5&   
   $��Your IP��6   $��2��6"   $��
0,178,31,0q�62   $��Right=�6F   $��68��6b   $��26��6m   $	��Top=�6y   $��2��6�   5(      5)      