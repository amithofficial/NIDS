Network Intrusion Detection System (NIDS) using GAN

A deep learning-based Network Intrusion Detection System (NIDS) that leverages **Generative Adversarial Networks (GANs)** to enhance detection performance by generating synthetic attack data and improving model robustness.

## 📌 Overview
Traditional NIDS often struggle with **imbalanced datasets**, where attack samples are far fewer than normal traffic samples. This project uses GANs to **generate realistic synthetic attack traffic**, improving the classifier's ability to detect rare or emerging attacks.

The system is designed to:
- Detect various network intrusions.
- Reduce false negatives by improving attack representation in training.
- Support real-time and batch processing.

---

## 🚀 Features
- **Data Augmentation**: GAN-generated attack traffic to balance datasets.
- **Multi-Class Detection**: Identifies multiple intrusion categories.
- **Real-Time Capability**: Can be adapted for streaming network traffic.
- **Model Flexibility**: Supports CNN, LSTM, or hybrid classifiers.
- **Extensible**: Can integrate with existing security tools (Snort, Suricata).
