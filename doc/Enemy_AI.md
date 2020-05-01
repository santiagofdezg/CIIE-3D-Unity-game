# Características da IA dos enemigos

Os enemigos poden estar en dous estados: aware (consciente), que indica que o enemigo sabe da existencia do xogador e vai a atacalo; e unaware (inconsciente), onde o enemigo deambula pola escena de xeito aleatorio.

### Características da IA:

- Campo de visión: se o xogador está dentro do campo de visión e a menor distancia que a distancia de visión do zombie, este pasa a estado aware (conciente).

- Capacidade auditiva: os enemigos poden escoitar ao xogador ou ás accións que realiza, é dicir, o xogador dependendo da acción que realiza fai unha cantidade de ruido que abarca un radio determinado. Por exemplo, cando o xogador fai un disparo xérase unha "esfera de ruido" e se hai algún enemigo dentro, este pasa a estado aware.

- Detección de presencia: un enemigo pode detectar a presencia do xogador cando está cerca aínda que non o vexa. Cando o xogador está andando pode achegarse máis aos enemigos sen que o detecten, pero se está correndo os enemigos detéctano dende máis lonxe.

- "Deambulación": mestres o enemigo non detecta ao xogador, este deambula pola escena de xeito aleatorio.

- Recorrido basado en puntos de referencia: algúns enemigos en lugar de deambular, terán un recorrido fixo marcado por varios puntos de referencia. Hai unha variable que establece se deambulan aleatoriamente ou se seguen un recorrido marcado.

### Modificación dalgúns parámetros

- Nav Mesh Agent
	- Velocidade
	- Velocidade angular (velocidade á que fai os xiros)
	- Aceleración


### Pendente de facer
- Aos zombies de menor nivel probar a facelos máis tontos: 
	- Menor capacidade de esquivar obstáculos -> Nav Mesh Agent
	- Menor distancia de detección
	- ...