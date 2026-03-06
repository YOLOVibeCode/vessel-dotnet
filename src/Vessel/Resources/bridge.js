// Vessel Bridge - Keyboard shortcuts, title observation, and navigation relay
(function () {
    'use strict';

    const isMac = navigator.platform.toUpperCase().indexOf('MAC') >= 0;
    const modKey = isMac ? 'metaKey' : 'ctrlKey';

    function send(type, data) {
        const msg = data !== undefined ? JSON.stringify({ type, data }) : JSON.stringify({ type });
        window.external.sendMessage(msg);
    }

    const iframe = document.getElementById('vessel-frame');

    // Title observation via MutationObserver on the iframe
    function observeTitle() {
        try {
            const iframeDoc = iframe.contentDocument || iframe.contentWindow.document;
            const titleEl = iframeDoc.querySelector('title');
            if (titleEl) {
                // Send initial title
                if (titleEl.textContent) {
                    send('titleChanged', titleEl.textContent);
                }
                const observer = new MutationObserver(function () {
                    if (titleEl.textContent) {
                        send('titleChanged', titleEl.textContent);
                    }
                });
                observer.observe(titleEl, { childList: true, characterData: true, subtree: true });
            }
        } catch (e) {
            // Cross-origin: can't observe title
        }
    }

    if (iframe) {
        iframe.addEventListener('load', observeTitle);
    }

    function handleKeyDown(e) {
        if (!e[modKey]) return;

        // Cmd/Ctrl+R - Reload
        if (e.key === 'r' && !e.shiftKey) {
            e.preventDefault();
            try { iframe.contentWindow.location.reload(); } catch (ex) { }
            return;
        }

        // Cmd/Ctrl+Shift+R - Hard Reload
        if (e.key === 'R' && e.shiftKey) {
            e.preventDefault();
            try {
                const url = iframe.contentWindow.location.href;
                iframe.contentWindow.location.replace(url);
            } catch (ex) { }
            return;
        }

        // Cmd/Ctrl+[ - Back
        if (e.key === '[' && !e.shiftKey) {
            e.preventDefault();
            try { iframe.contentWindow.history.back(); } catch (ex) { }
            return;
        }

        // Cmd/Ctrl+] - Forward
        if (e.key === ']' && !e.shiftKey) {
            e.preventDefault();
            try { iframe.contentWindow.history.forward(); } catch (ex) { }
            return;
        }

        // Cmd/Ctrl++ or Cmd/Ctrl+= - Zoom In
        if (e.key === '+' || e.key === '=') {
            e.preventDefault();
            send('zoomIn');
            return;
        }

        // Cmd/Ctrl+- - Zoom Out
        if (e.key === '-') {
            e.preventDefault();
            send('zoomOut');
            return;
        }

        // Cmd/Ctrl+0 - Reset Zoom
        if (e.key === '0') {
            e.preventDefault();
            send('resetZoom');
            return;
        }

        // Cmd/Ctrl+Option/Alt+I - Dev Tools (no-op in bridge, handled natively if available)
        if (e.key === 'i' && e.altKey) {
            e.preventDefault();
            return;
        }

        // Ctrl+Cmd+F (Mac) or Ctrl+F11 / F11 (other) - Fullscreen
        if (isMac && e.key === 'f' && e.ctrlKey && e.metaKey) {
            e.preventDefault();
            send('toggleFullScreen');
            return;
        }
        if (!isMac && e.key === 'F11') {
            e.preventDefault();
            send('toggleFullScreen');
            return;
        }

        // Cmd/Ctrl+Option/Alt+T - Always on Top
        if (e.key === 't' && e.altKey) {
            e.preventDefault();
            send('toggleTopMost');
            return;
        }

        // Cmd/Ctrl+M - Minimize
        if (e.key === 'm' && !e.shiftKey && !e.altKey) {
            e.preventDefault();
            send('minimize');
            return;
        }

        // Cmd/Ctrl+Q - Quit
        if (e.key === 'q' && !e.shiftKey && !e.altKey) {
            e.preventDefault();
            send('quit');
            return;
        }
    }

    // Listen on the main document
    document.addEventListener('keydown', handleKeyDown);

    // Also try to listen on the iframe's document for keyboard events
    function attachIframeKeyListener() {
        try {
            iframe.contentDocument.addEventListener('keydown', handleKeyDown);
        } catch (e) {
            // Cross-origin: can't attach listener
        }
    }

    if (iframe) {
        iframe.addEventListener('load', attachIframeKeyListener);
    }
})();
