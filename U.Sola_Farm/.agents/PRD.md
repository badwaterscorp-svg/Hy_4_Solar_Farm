# Product Requirements Document

## Nombre del producto
AI Excel Assistant

## Objetivo
Crear una aplicación que permita hacer preguntas sobre un archivo Excel y obtener respuestas usando IA.

## Usuarios
Personas que necesitan consultar información de hojas de cálculo sin usar fórmulas.

## Funcionalidades (Enfoque)

### 1. Cargar Excel
El usuario puede subir un archivo .xlsx.

### 2. Procesar datos
El sistema convierte el Excel en una estructura que pueda leer el LLM.

### 3. Chat
El usuario puede hacer preguntas en lenguaje natural como:
- ¿Cuál fue el total de ventas en enero?
- ¿Qué producto vendió más?

### 4. Respuesta
El sistema responde con texto o tablas.

## Tecnologías

Frontend:
- HTML
- Vanilla JS

Backend:
- Node.js
- n8n
- LLM local (Ollama)

## Restricciones

- Debe correr localmente
- No usar servicios pagos