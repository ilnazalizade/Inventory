   M S B A M L   `   `   `  ????B  >IMSBeta, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:IMSBeta.windowIMSBeta.window  5   	   !    IMSBeta.window.win_customer   5   	   ` \RibbonControlsLibrary, Version=3.5.41019.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:IMSBeta.window     5   	   -   5          ? Title$  win_customer??5      $	??700??6   $	??600??6%    ? WindowStartupLocation$ CenterScreen=?61    ? WindowStyle$
 None=?6V   
??????  ? AllowsTransparency .  ? Triggers 5      :? 5   
    :? RoutedEvent$ Loaded??6   .[?5      ?? .??5      ??  g? Duration$ 00:00:00:01D?6    ?? TargetProperty$ Opacity?64   .??5      W?  W? From$ 0X?6&   		 W? To$	 3X?6/   5      5      5      .??5      ? .+?5   
   ?? $??Center=?6   $	??680??61   
???   ?
???????$??Center=?6Z   $	??580??6u   ?? 5   
   -   5        $??Center=?5      $??88??61   $??0,10,0,0q?6=   
 ?? RadiusY$
 10??6O    ?? RadiusX$ 10??6\   
???   ?$	??Top=?6x   $	??580??6?    J? Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      ??5      ?? 5       ?? EndPoint$ 0.5,1&?6&    ?? 
StartPoint$ 0.5,0&?67   .??5      ?  ? Color$ 	#FF7CB2F1??6#    ? Offset$ 0Q?65   ? 5      $ White??6#   $ 1Q?61   5      5!      ?? 5#   
   $??	Customers??6   $??110,37,352,0q?6%   $	??Top=?6;   $
??Bold"?6S   $??18(?6e   ?? 5$   
   $
??Left=?6   $??58??6+   $??	31,21,0,0q?67   $	??Top=?6J   $??70??6b   $??/img/customer.png??6m   ?? 5%   
   -   5        $
??Left=?5%      $??16??6+   $??
550,44,0,0q?67   $	??Top=?6K   $??20??6c    J? RenderTransformOrigin$ 
0.7,-0.062&?6n   $??/img/exit.png??5&       ? Cursor$
 Hand??6'   65   &   System.Windows.Controls.DataGrid  5)   
   -   5        $??DataGridCustomer??5)         AutoGenerateColumns .  $??32,121,34,122q?6G   $??1q?6^      VerticalGridLinesBrush
 ?????   Columns 5+      0  *System.Windows.Controls.DataGridTextColumn  5-      ,  &System.Windows.Controls.DataGridColumn   Header$ Customer ID??6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6:   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6F   ?? *
CustomerID+  50      $
 Name??6%   $	 100 63    6?   ?? *CustomerName+  53      $	 Tel??6%   $	 100 62    6>   ?? *CustomerTel+  56      $ Address??6%   $	 130 66    6B   ?? *CustomerAddress+  57      $ CustomerActive??6%   $	 100 6=    6I   ?? *CustomerActive+ ? ContextMenu 5=      ?? 5>      .@?5?      m? -   5        $??Active Customer??5?       m? Icon 5@      ?? 5A      $??/img/price.png??6$   $??12??6<   $??12??6G   5C      5E      5J      ?? 5O   
   -   5         ??Name$ Btn_EditCustomer??5O      $??Edit Customr??6+   $??Center=?6C   $??	0,609,0,0q?6`   $	??Top=?6s   $	??110??6?   $??37??6?   $??14.667(?6?   ???	#FF3E3709$
 Hand??6?    5P        5Q      ??5S      ?? 5T      $ 0.5,1&?6&   $ 0.5,0&?67   .??5U      ? $ 	#FF6C8EC9??6#   $ 0Q?65   ? 5V      $ 	#FF224258??6#   $ 1Q?65   ? 5W      $ 	#FFDAEBF3??6#   $ 0.783Q?65   5X      5Z      ?? 5^   
   -   5        $ Btn_AddCustomer??5^      $??Add Customer??6*   $
??Left=?6A   $??
81,609,0,0q?6\   $	??Top=?6p   $	??109??6?   $??37??6?   $??14.667(?6?   ???	#FF3E3709$
 Hand??6?    5_        5`      ??5b      ?? 5c      $ 0.5,1&?6&   $ 0.5,0&?67   .??5d      ? $ 	#FF6C8EC9??6#   $ 0Q?65   ? 5e      $ 	#FF224258??6#   $ 1Q?65   ? 5f      $ 	#FFDAEBF3??6#   $ 0.783Q?65   5g      5i      ?? 5k   
   -   5        $ Btn_DeleteCustomer??5k      $??Delete Customer??6-   $??418,610,74,0q?6H   $	??Top=?6^   $	??108??6v   $??37??6?   $??14.667(?6?   ???	#FF3E3709$
 Hand??6?    5l        5m      ??5o      ?? 5p      $ 0.5,1&?6&   $ 0.5,0&?67   .??5q      ? $ 	#FF6C8EC9??6#   $ 0Q?65   ? 5r      $ 	#FF224258??6#   $ 1Q?65   ? 5s      $ 	#FFDAEBF3??6#   $ 0.783Q?65   5t      5v      5z      5{      