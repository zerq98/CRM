   M S B A M L   `   `   `  ����U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  5      K GCRM.Desktop.View, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:CRM.Desktop.View.ViewsCRM.Desktop.View.Views 5      I)clr-namespace:CRM.Desktop.View.ConvertersCRM.Desktop.View.Converters 5      &    CRM.Desktop.View.Views.StatsView   5      \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5      6local$clr-namespace:CRM.Desktop.View.Views     5      @
converters)clr-namespace:CRM.Desktop.View.Converters     5      -   5        $��Window��5
   +     � 	Resources  5      /  )CRM.Desktop.View.Converters.SizeConverter   sizeConverter��%   &	          5   
   6#   5      .��5      ��  �� CornerRadius$ 	0,0,10,10��6   ._�5   
   � ���	#B2FFFFFF.+�5      �� $ 10,10,10,10��6   
���   �$��1q�6D   $��10q�6X   ��6d   �� *ActualWidth+ �� 	Converter# [   �� ElementName$ Window�� �� Mode$ OneWay=���6�   �� *ActualHeight+# [  $ Window��$ OneWay=�._�5      #   LiveCharts.Wpf.CartesianChart  &    LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$
 None=�6S      Series 6%   �� *OpportunityCountCollection+   AxisX 5         LiveCharts.Wpf.Axis     Labels9   3LiveCharts.Wpf.Converters.StringCollectionConverter$6 0Nowa,Modyfikowana,Anulowana,Zaakceptowana,Oferta 6#   	   	Separator	 5         LiveCharts.Wpf.Separator  5   "   ${�False��60   
   Step$
 1Q�6B   6M   5         AxisY 5        	 5        5   "   $
{�True��60   $
 1Q�6A   6L   5      5       5!      �� 5"      $ 10,10,10,10��6   
���   �$��1q�6D   $��10q�6X   ��6d   �� *ActualWidth+# [  $ Window��$ OneWay=���6�   �� *ActualHeight+# [  $ Window��$ OneWay=�._�5#         LiveCharts.Wpf.PieChart  $	 Top=�6    64   �� *ContactsCollection+5$      5%      �� 5&      $ 10,10,10,10��6   
���   �$��1q�6D   $��10q�6X   ��6d   �� *ActualWidth+# [  $ Window��$ OneWay=���6�   �� *ActualHeight+# [  $ Window��$ OneWay=�._�5'        $
 None=�6Q    6%   �� *ThisMonthCountCollection+ 5)        $ Netto,Brutto,Marża 6#   	 5*        5+   "   ${�False��60   $
 1Q�6B   6M   5-       50        	 51        52   "   $
{�True��60   $

 5000Q�6A   6O   54      56      57      �� 58      $ 10,10,10,10��6   
���   �$��1q�6D   $��10q�6X   ��6d   �� *ActualWidth+# [  $ Window��$ OneWay=���6�   �� *ActualHeight+# [  $ Window��$ OneWay=�._�59        $
 None=�6P    6%   �� *ThisYearCountCollection+ 5;        $ Netto,Brutto,Marża 6#   	 5<        5=   "   ${�False��60   $
 1Q�6B   6M   5?       5B        	 5C        5D   "   $
{�True��60   $

 5000Q�6A   6O   5F      5H      5I      5J      5K      5L      