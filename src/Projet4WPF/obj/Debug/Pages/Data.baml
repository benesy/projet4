   M S B A M L   `   `   `  ����E  AProjet4WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Projet4WPF.PagesProjet4WPF.Pages  5          Projet4WPF.Pages.Data   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:Projet4WPF.Pages     5        N� Title$
  Data��5	      .��5
      � .+�5   
   o� .@�5      m� -   5        $�� Accueil��5      6)   m� 5      -   5        $��	 Importer��5      6*   m� 5      $�� Tableau de données��6   m� 5      -   5        $��
 Graphique��5      6+   m� 5      $��	 Exporter��6   .@�5      m� -   5        $	��CSV��5      6(   m� 5      -   5        $	��PDF��5      6(   5      5      � 5   
   $��10,0,10,266q�6   $	��130��6$   $��Bottom=�61    � ColumnDefinitions 5      �� 5      $	��13* �6#   �� 5      $	��17* �6#   .+�5      �� $��N° Série du Capteur:��6   $��
0,0,154,85q�63   $��33��6G   $��Bottom=�6S   $��Right=�6n   $	��130��6�   �� 5      -   5         ��Name$ 	capteurSN��5      $
��XXXX��6+   $��
206,0,0,87q�67   $��30��6K   $��Bottom=�6W   �� 5      $��Nombre d'entrées:��6   $��
0,0,154,36q�60   $��32��6D   $��Bottom=�6P   $��Right=�6k   $	��130��6�   �� 5      -   5        $ 
nbrEntries��5      $
��XXXX��6,   $��206,0,58,36q�68   $��27��6M   $��Bottom=�6Y   �� 5       -   5         ��Name$
 lab1��5       $��Début��6"   $��56,0,300,87q�62   $��30��6G   $��Bottom=�6S   $��1��6n   (  "System.Windows.Controls.DatePicker  5!      -	   5          Name$ debut��5!      $��32��6(   $	��150��64   $��
172,0,0,85q�6�   $��Bottom=�6�   $
��Left=�6�   $��1��6�      SelectedDate 6@   �� *
DateTime+ �� StringFormat$ dd/MM/yyyy HH:mm:ss��6�   �� 5"      -
   5        $
 lab2��5"      $	��Fin��6"   $��56,0,312,36q�60   $��30��6E   $��Bottom=�6Q   $��1��6l     5#      -   5        $	 fin��5#      $��30��6&   $	��150��62   $��
172,0,0,36q�6�    J� RenderTransformOrigin$ 	0.943,0.1&�6�   $��Bottom=�6�   $
��Left=�6�   $��1��6�    6>   �� *
DateTime+$ dd/MM/yyyy HH:mm:ss��6   5$      � 5%   
   $��
0,189,10,0q�6   $	��251��6#   $	��Top=�60    5&      �� 5'      $	��89* �6#   �� 5(      $	��69* �6#   .+�5*      &   System.Windows.Controls.DataGrid  -   5          Name$ tableau��5*      $��10,10,0.4,39q�6(   $��2��6>   5+      5,      5-      