<script lang="ts">
  import { onMount } from 'svelte';
  import * as signalR from '@microsoft/signalr';

  let username = '';
  let connected = false;
  let connection: signalR.HubConnection | null = null;

  onMount(() => {
    connection = new signalR.HubConnectionBuilder()
      .withUrl('/hubs/canvas')
      .withAutomaticReconnect()
      .build();

    connection.on('PixelPlaced', (pixel: { x: number; y: number; color: string }) => {
      console.log('Pixel placed:', pixel);
    });

    connection.start()
      .then(() => {
        connected = true;
        console.log('SignalR connected');
      })
      .catch(err => console.error('SignalR error:', err));

    return () => {
      connection?.stop();
    };
  });

  async function placePixel(x: number, y: number, color: string) {
    if (connection && connected) {
      try {
        await connection.invoke('PlacePixel', { x, y, color });
      } catch (err) {
        console.error('Error placing pixel:', err);
      }
    }
  }
</script>

<div class="min-h-screen bg-gray-900 text-white p-8">
  <h1 class="text-4xl font-bold mb-8">Pixel Canvas</h1>
  
  <div class="mb-4">
    <input 
      type="text" 
      bind:value={username}
      placeholder="Enter username"
      class="bg-gray-800 border border-gray-700 rounded px-4 py-2"
    />
  </div>

  <div class="status mb-4">
    <span class:text-green-500={connected} class:text-red-500={!connected}>
      {connected ? 'Connected' : 'Connecting...'}
    </span>
  </div>

  <div class="canvas grid grid-cols-20 gap-0 w-fit border-2 border-gray-700">
    {#each Array(20) as _, y}
      {#each Array(20) as _, x}
        <button
          class="w-6 h-6 border border-gray-800 hover:opacity-80 transition-opacity"
          style="background-color: #1a1a2e"
          on:click={() => placePixel(x, y, '#ff0000')}
        />
      {/each}
    {/each}
  </div>
</div>
