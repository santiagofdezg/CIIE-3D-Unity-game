# Características da IA dos enemigos

Os enemigos poden estar en dous estados: aware (consciente), que indica que o enemigo sabe da existencia do xogador e vai a atacalo; e unaware (inconsciente), onde o enemigo deambula pola escena de xeito aleatorio.

### Características da IA:

- Campo de visión: se o xogador está dentro do campo de visión e a menor distancia que a distancia de visión do zombie, este pasa a estado aware (conciente).

- Capacidade auditiva: os enemigos poden escoitar ao xogador ou ás accións que realiza, é dicir, o xogador dependendo da acción que realiza fai unha cantidade de ruido que abarca un radio determinado. Por exemplo, cando o xogador fai un disparo xérase unha "esfera de ruido" e se hai algún enemigo dentro, este pasa a estado aware. Cada acción pode xerar unha esfera de ruido de diferente tamaño.

- Detección de presencia: un enemigo pode detectar a presencia do xogador cando está cerca aínda que non o vexa. Cando o xogador está andando pode achegarse máis aos enemigos sen que o detecten, pero se está correndo os enemigos detéctano dende máis lonxe.

- "Deambulación": mestres o enemigo non detecta ao xogador, este deambula pola escena de xeito aleatorio.

- Recorrido basado en puntos de referencia: algúns enemigos en lugar de deambular, terán un recorrido fixo marcado por varios puntos de referencia. Hai unha variable que establece se deambulan aleatoriamente ou se seguen un recorrido marcado.

- Pérdida do xogador: se o enemigo está perseguindo ao xogador e o perde de vista durante x segundo, o enemigo volve ao estado de "deambulación".

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

- Zombies que estean en estado "biting" (animación en mixamo) e que desperten ao detectar ao xogador

# Animacións e modelo de zombie
Se utilizamos Mixamo, a carpeta de texturas conséguese descargando o modelo en formato Collada (.dae), dentro trae unha carpeta coas texturas que imos a utilizar. O modelo en si descárgase en formato "FBX for Unity". Para as animacións asegurarse de marcar a opción "In Place" dentro da configuración da animación en Mixamo e descárganse en formato "FBX for Unity" coa opción "Without skin".

Partimos do prefab "Enemy" que está formado por unha cápsula. Na carpeta do novo zombie hai que pegar o modelo descargado e unha carpeta coas texturas.

#### Preparar o modelo e as animacións
- Click no modelo descargado e ir ao inspector
- Na pestaña "Rig", poñer o tipo de animación a "Humanoid" e crear un avatar a partir do modelo. Darlle a "Apply".
- É necesario pasar a "Humanoid" tanto o modelo principal como os modelos que conteñen dentro as animacións.
- Extraer a animación de cada modelo: click na animación e duplicala (Ctrl+D)
- Configurar cada animación:
	- Loop time: On
	- Bake Into Pose: On (En todos os apartados). Esta opción non está moi clara pero noutras animacións do asset store para zombies veñen todas activadas.

### Importar modelo e animacións

#### Modelo
- Arrastrar o prefab Enemy á escena
- Desactivar o obxecto cubo pero non eliminalo para saber cal é a parte dianteira do zombie
- Desactivar a compoñente "Mesh Renderer" do zombie para que non se renderize a cápsula.
- Arrastrar o modelo do novo zombie dentro do obxecto Enemy que xa tiñamos
- Mover o modelo e escalalo se é necesario ata que coincida coa altura do obxecto Zombie (cilindro do NavMeshAgent e cápsula do collider). Tamén se pode axustar a altura da compoñente "Capsule collider" e o cilindro da compoñente NavMeshAgent. Todo isto poderase volver a modificar máis tarde.

#### Animacións
- Crear unha carpeta "animations" e crear dentro un "Animator controller"
- Arrastrar o ZombieController ao "Controller" da compoñente "Animator" de modelo
- Doble click en ZombieController para ir á ventana de Animator.
- Arrastrar a animación á ventana do Animator
- Se queremos engadir máis animacións hai que arrastralas ao Animator e establecer as transicións entre elas.
- Unha vez están establecidas as transicións con todas as flechas, hai que indican que provoca o cambio de unha a outra.
- Por exemplo, para o cambio de "Walk" a "Running" hai que crear un novo parámetro de tipo bool dentro do Animator que se chamará "Aware" e indica se o enemigo está en estado de consciencia ou non. O valor desta variable cambiarase dentro do script.
- Facemos click na flecha que vai de "Walk" a "Running" e desactivamos "Has Exit Time" no inspector.
- Na lista de condicións engadimos a variable Aware con valor true.
- Coa flecha que vai de "Running" a "Walk" facemos o mesmo pero poñendo Aware a false.

