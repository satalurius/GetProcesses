# GetProcesses
По идее собирает данные с компьютера и отправляет на сервер

C Божьей помощью работает, за остальное не уверен


### Как это работает:
  Приложение клиент запускается на машине с которой хотим собрать процессы </br>
  Приложение сервер, запущенная на машине, на которой храним все данные о машинах, принимает поток данных по сети
  
  
 ### Проблемы:
  Данные не защищены, передача по сети, будет идти в не зашифрованном виде, поэтому лучше не использовать для чего то секретного :)
