
# Nav Mesh components
As compoñentes Nav Mesh utilízanse para regular por onde se pode andar na escena. Algunhas non veñen por defecto na instalación de Unity e hai que descargalas de GitHub: https://github.com/Unity-Technologies/NavMeshComponents . 

## Creación dunha superficie
[Tutorial](https://www.youtube.com/watch?v=CHV1ymlw-P8&list=PLgOGMxhUTm3wuPzAM9QvQF792XAD6TM-i)

- Crear un GameObject vacio na escena (chamalo NavMesh) e engadirlle a compoñente NavMeshSurface
- En "Include Layers" eliminar a capa Player
- Darlle a "Bake"

#### Facer que non se poida andar por un obxecto
Hai dúas maneiras de facelo (preferiblemente a segunda):
1. Ignorar o obxecto ao facer o Bake
	- Engadir nese obxecto a compoñente NavMeshModifier
	- Marcar a opción "Ignore From Build"
	- Volver a facer un "Bake" no NavMesh

2. Cambiar o tipo de área do obxecto
	- Engadir no obxecto a compoñente NavMeshModifier
	- Marcar "Override Area"
	- Cambiar o tipo de área segundo o que interese 