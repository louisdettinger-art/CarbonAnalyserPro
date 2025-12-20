// Importation des fonctions Firebase
import { initializeApp } from "https://www.gstatic.com/firebasejs/10.7.1/firebase-app.js";
import { getAuth } from "https://www.gstatic.com/firebasejs/10.7.1/firebase-auth.js";
import { getFirestore } from "https://www.gstatic.com/firebasejs/10.7.1/firebase-firestore.js";

// --- REMPLACEZ CECI PAR VOS PROPRES CLÉS (Dispo sur la console Firebase) ---
const firebaseConfig = {
    apiKey: "AIzaSyCKAiMu4FdQrQMf28MPR_dKpiCv6vpklo4",
    authDomain: "carbonindexpro.firebaseapp.com",
    projectId: "carbonindexpro",
    storageBucket: "carbonindexpro.firebasestorage.app",
    messagingSenderId: "838442948806",
    appId: "1:838442948806:web:6ec91e27201c293f7f1fc7"
};

// Initialisation
const app = initializeApp(firebaseConfig);
const auth = getAuth(app);
const db = getFirestore(app);

// On exporte les outils pour les utiliser dans les autres pages
export { auth, db };